using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QuestPDF.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.UTILS;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDespesaPeriodoTecnico(DespesaPeriodoTecnicoParameters parameters)
		{
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);

            var sheet = despesas.Select(d => new {
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
            
            var itens = _despesaService.Impressao(new DespesaParameters {
                CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                CodTecnico = despesa.CodTecnico.ToString(),
                indAtivo = 1
            });

            var adiantamentos = _despesaAdiantamentoPeriodoRepo.ObterPorParametros(new DespesaAdiantamentoPeriodoParameters {
                CodDespesaPeriodo = despesa.CodDespesaPeriodo,
                CodTecnico = despesa.CodTecnico,
                IndAdiantamentoAtivo = 1
            });

            var despesaImpressao = new DespesaPeriodoTecnicoPdfHelper(despesa, itens, adiantamentos);
            var despesaPdf = GenerateFilePath($"DESPESA-{despesa.CodDespesaPeriodoTecnico}.pdf");
            despesaImpressao.GeneratePdf(despesaPdf);
            byte[] file = File.ReadAllBytes(despesaPdf);
            return new FileContentResult(file, "application/pdf");
        }
    }
}