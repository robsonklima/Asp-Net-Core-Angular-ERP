using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QuestPDF.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDespesaPeriodoTecnico(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);

            var sheet = despesas.Select(d => new
            {
                CodDespesaPeriodoTecnico = d.CodDespesaPeriodoTecnico,
                Protocolo = d.DespesaProtocoloPeriodoTecnico?.CodDespesaProtocolo,
                Tecnico = d.Tecnico?.Nome,
                Status = d.DespesaPeriodoTecnicoStatus?.NomeDespesaPeriodoTecnicoStatus,
                InicioPeriodo = d.DespesaPeriodo.DataInicio,
                FimPeriodo = d.DespesaPeriodo.DataFim,
                ProcessadoJuntoATicket = d.TicketLogPedidoCredito?.IndProcessado == 1 ? "SIM" : "NÃO",
                DataHoraProcessamentoTicket = d.TicketLogPedidoCredito?.DataHoraProcessamento,
                NumeroCartao = d.TicketLogPedidoCredito?.NumeroCartao,
                Observacao = d.TicketLogPedidoCredito?.Observacao,
                Creditado = d.IndCredito == 1 ? "SIM" : "NÃO",
                DataHoraCredito = d.DataHoraCredito,
                UsuarioCredito = d.CodUsuarioCredito,
                Compensado = d.IndCompensacao == 1 ? "SIM" : "NÃO",
                DataHoraCompensacao = d.DataHoraCompensacao,
                UsuarioCompensacao = d.CodUsuarioCompensacao,
                Verificado = d.IndVerificacao == 1 ? "SIM" : "NÃO",
                DataHoraVerificacao = d.DataHoraVerificacao,
                UsuarioVerificacao = d.CodUsuarioVerificacao,
            });

            var wsOs = Workbook.Worksheets.Add("despesas");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }

        private IActionResult GerarPdfDespesaPeriodoTecnico(Exportacao exportacao)
        {
            var parameters = ((JObject)exportacao.EntityParameters).ToObject<DespesaPeriodoTecnicoParameters>();

            var despesa = _despesaPeriodoTecnicoRepo.ObterPorCodigo((int)parameters.CodDespesaPeriodoTecnico);

            var itens = _despesaService.Impressao(new DespesaParameters
            {
                CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                CodTecnico = despesa.CodTecnico.ToString(),
                indAtivo = 1
            });

            var adiantamentos = _despesaAdiantamentoPeriodoRepo.ObterPorParametros(new DespesaAdiantamentoPeriodoParameters
            {
                CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                CodTecnico = despesa.CodTecnico,
                IndAdiantamentoAtivo = 1
            });

            decimal despesaKM = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor);
            decimal despesaOutros = itens.Where(i => i.CodDespesaTipo != (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor);
            decimal totalDespesa = despesaOutros + despesaKM;
            decimal adiantamentoRecebido = adiantamentos.Sum(i => i.DespesaAdiantamento.ValorAdiantamento);
            decimal adiantamentoUtilizado = adiantamentos.Sum(i => i.ValorAdiantamentoUtilizado);

            DespesaPeriodoTecnicoImpressaoModel despesaPeriodoTecnicoModel = new DespesaPeriodoTecnicoImpressaoModel
            {
                Despesa = despesa,
                Adiantamentos = adiantamentos,
                Itens = itens,
                AluguelCarro = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ALUGUEL_CARRO).Sum(i => i.DespesaValor),
                Correio = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.CORREIO).Sum(i => i.DespesaValor),
                Frete = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.FRETE).Sum(i => i.DespesaValor),
                Outros = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.OUTROS).Sum(i => i.DespesaValor),
                Pedagio = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PEDAGIO).Sum(i => i.DespesaValor),
                Taxi = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.TAXI).Sum(i => i.DespesaValor),
                CartaoTelefonico = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.CARTAO_TEL).Sum(i => i.DespesaValor),
                Estacionamento = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ESTACIONAMENTO).Sum(i => i.DespesaValor),
                Hotel = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.HOTEL).Sum(i => i.DespesaValor),
                PassagemAerea = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PA).Sum(i => i.DespesaValor),
                CartaoCombustivel = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.KM).Sum(i => i.DespesaValor),
                Telefone = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.TELEFONE).Sum(i => i.DespesaValor),
                Combustivel = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.COMBUSTIVEL).Sum(i => i.DespesaValor),
                Ferramentas = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.FERRAMENTAS).Sum(i => i.DespesaValor),
                Onibus = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.ONIBUS).Sum(i => i.DespesaValor),
                PecasComponentes = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.PECAS).Sum(i => i.DespesaValor),
                Refeicao = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.REFEICAO).Sum(i => i.DespesaValor),
                Internet = itens.Where(i => i.CodDespesaTipo == (int)DespesaTipoEnum.INTERNET).Sum(i => i.DespesaValor),
                DespesaKM = despesaKM,
                DespesaOutros = despesaOutros,
                TotalDespesa = totalDespesa,
                AdiantamentoRecebido = adiantamentoRecebido,
                AdiantamentoUtilizado = adiantamentoUtilizado,
                AReceberViaDeposito = despesaOutros - adiantamentoUtilizado < 0 ? 0 : despesaOutros - adiantamentoUtilizado,
                SaldoAdiantamento = adiantamentoRecebido - adiantamentoUtilizado,
                PercentualOutros = Math.Round((despesaOutros / (totalDespesa == 0 ? 1 : totalDespesa) * 100), 2),
                PercentualDespesaCB = Math.Round((despesaKM / (totalDespesa == 0 ? 1 : totalDespesa) * 100), 2)
            };

            var despesaImpressao = new DespesaPeriodoTecnicoPdfHelper(despesaPeriodoTecnicoModel);
            var despesaPdf = GenerateFilePath($"DESPESA-{despesa.CodDespesaPeriodoTecnico}.pdf");
            despesaImpressao.GeneratePdf(despesaPdf);
            byte[] file = File.ReadAllBytes(despesaPdf);
            return new FileContentResult(file, "application/pdf");
        }
    }
}