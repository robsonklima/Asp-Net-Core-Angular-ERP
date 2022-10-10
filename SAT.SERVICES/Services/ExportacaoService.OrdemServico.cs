using System.Linq;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using System;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SAT.UTILS;
using System.IO;
using QuestPDF.Fluent;
using System.Net;
using SelectPdf;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOrdemServico(OrdemServicoParameters parameters)
        {
            var os = _osRepo.ObterPorParametros(parameters);

            var viewUnificada = _osRepo.ObterViewPorOs(os.Select(os => os.CodOS).ToArray());

            var osSheet = os.Select(os =>
                             new
                             {
                                 Chamado = os.CodOS,
                                 NumOSCliente = os.NumOSCliente,
                                 DataAbertura = os.DataHoraAberturaOS?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 DataSolicitacao = os.DataHoraSolicitacao?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 LimiteAtendimento = os.PrazosAtendimento?.OrderByDescending(i => i.CodOSPrazoAtendimento)?.FirstOrDefault()?.DataHoraLimiteAtendimento?.ToString("dd/MM/yy HH:mm") ?? Constants.NENHUM_REGISTRO,
                                 Status = os.StatusServico?.NomeStatusServico?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Intervencao = os.TipoIntervencao?.CodETipoIntervencao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Tecnico = os.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                                 NumBanco = os.Cliente?.NumBanco ?? Constants.NENHUM_REGISTRO,
                                 NumAgencia = os.LocalAtendimento != null ? ($"{os.LocalAtendimento.NumAgencia}/{os.LocalAtendimento.DCPosto}") : Constants.NENHUM_REGISTRO,
                                 Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Cliente = os.EquipamentoContrato?.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                 Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                 Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                 Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                 Regiao = os.Regiao?.NomeRegiao?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 PA = os.RegiaoAutorizada?.PA ?? 0,
                                 Autorizada = os.Autorizada?.NomeFantasia?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Filial = os.Filial?.NomeFilial,
                                 SLA = os.EquipamentoContrato?.AcordoNivelServico?.NomeSLA ?? Constants.NENHUM_REGISTRO,
                                 Reincidencia = os.NumReincidencia ?? 0,
                                 Defeito = os.DefeitoRelatado ?? Constants.NENHUM_REGISTRO,
                                 Fechamento = os.DataHoraFechamento,
                             });

            var ratSheet = os.SelectMany(os => os.RelatoriosAtendimento.Select(r =>
                             new
                             {
                                 Chamado = r.CodOS,
                                 CodRat = r.CodRAT,
                                 NumRat = r.NumRAT ?? Constants.NENHUM_REGISTRO,
                                 Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                 Tecnico = r.Tecnico?.Nome ?? Constants.NENHUM_REGISTRO,
                                 Status = r.StatusServico?.NomeStatusServico ?? Constants.NENHUM_REGISTRO,
                                 DataInicio = r.DataHoraInicio.ToString() ?? Constants.NENHUM_REGISTRO,
                                 DataSolucao = r.DataHoraSolucao.ToString() ?? Constants.NENHUM_REGISTRO,
                                 Cliente = os.EquipamentoContrato?.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                 Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                 Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                 Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                 TipoServico = r.TipoServico?.NomeServico ?? Constants.NENHUM_REGISTRO,
                                 Observacao = r.ObsRAT ?? Constants.NENHUM_REGISTRO,
                                 RelatoSolucao = r.RelatoSolucao ?? Constants.NENHUM_REGISTRO,
                                 DataCadastro = r.DataHoraCad,
                             }
                        ));

            var ratDetalheSheet = os.SelectMany(os => os.RelatoriosAtendimento
                                    .SelectMany(rat => rat.RelatorioAtendimentoDetalhes
                                    .Select(d =>
                                        new
                                        {
                                            Chamado = rat?.CodOS,
                                            NumRat = rat?.CodRAT,
                                            Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                            Cliente = os.EquipamentoContrato?.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                            Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                            CodTipoCausa = d.TipoCausa?.CodETipoCausa ?? Constants.NENHUM_REGISTRO,
                                            TipoCausa = d.TipoCausa?.NomeTipoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodGrupoCausa = d.GrupoCausa?.CodEGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            GrupoCausa = d.GrupoCausa?.NomeGrupoCausa ?? Constants.NENHUM_REGISTRO,
                                            CodDefeito = d.Defeito?.CodEDefeito ?? Constants.NENHUM_REGISTRO,
                                            Defeito = d.Defeito?.NomeDefeito ?? Constants.NENHUM_REGISTRO,
                                            CodCausa = d.Causa?.CodECausa ?? Constants.NENHUM_REGISTRO,
                                            Causa = d.Causa?.NomeCausa ?? Constants.NENHUM_REGISTRO,
                                            CodAcao = d.Acao?.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                            Acao = d.Acao?.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                            OrigemCausa = d.CodOrigemCausa != null ? d.CodOrigemCausa.Value == 1 ? "Máquina" : "Extra Máquina" : "Causa Origem Não Informada"
                                        }
                                    )));

            var ratDetalhePecaSheet = os.SelectMany(os => os.RelatoriosAtendimento
                                        .SelectMany(rat => rat.RelatorioAtendimentoDetalhes
                                        .SelectMany(rp => rp.RelatorioAtendimentoDetalhePecas
                                        .Select(p =>
                                        new
                                        {
                                            Chamado = rat.CodOS,
                                            NumRat = rat.NumRAT ?? Constants.NENHUM_REGISTRO,
                                            Cidade = os.LocalAtendimento?.Cidade?.NomeCidade?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            UF = os.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Local = os.LocalAtendimento?.NomeLocal?.ToUpperInvariant() ?? Constants.NENHUM_REGISTRO,
                                            Regiao = os.Regiao?.NomeRegiao ?? Constants.NENHUM_REGISTRO,
                                            Filial = os.Filial?.NomeFilial ?? Constants.NENHUM_REGISTRO,
                                            Cliente = os.EquipamentoContrato?.Cliente?.NomeFantasia ?? Constants.NENHUM_REGISTRO,
                                            Equipamento = os.Equipamento?.NomeEquip ?? Constants.NENHUM_REGISTRO,
                                            Serie = os.EquipamentoContrato?.NumSerie ?? Constants.NENHUM_REGISTRO,
                                            Contrato = os.EquipamentoContrato?.Contrato?.NroContrato ?? Constants.NENHUM_REGISTRO,
                                            CodAcao = rp.Acao?.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                            Acao = rp.Acao?.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                            CodMagnus = p.Peca.CodMagnus ?? Constants.NENHUM_REGISTRO,
                                            Peca = p.Peca.NomePeca ?? Constants.NENHUM_REGISTRO,
                                            QuantidadePecas = p.QtdePecas
                                        }
                                    ))));

            var exportUnificado = viewUnificada.Select(v =>
                                            new
                                            {
                                                CodOS = v.CodOS.HasValue ? v.CodOS.Value.ToString() : Constants.NENHUM_REGISTRO,
                                                NumOSCliente = v.NumOSCliente ?? Constants.NENHUM_REGISTRO,
                                                NumOSQuarteirizada = v.NumOSQuarteirizada ?? Constants.NENHUM_REGISTRO,
                                                DataHoraAberturaOS = v.DataHoraAberturaOS.HasValue ? v.DataHoraAberturaOS.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                DataHoraSolicitacao = v.DataHoraSolicitacao.HasValue ? v.DataHoraSolicitacao.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                DataHoraFechamento = v.DataHoraFechamento.HasValue ? v.DataHoraFechamento.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                DataHoraAgendamento = v.DataAgendamento.HasValue ? v.DataAgendamento.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                HorasOsAberta = (v.DataHoraFechamento.HasValue) ?
                                                                v.DataHoraFechamento.Value.Subtract(v.DataHoraAberturaOS.Value).TotalHours :
                                                                DateTime.Now.Subtract(v.DataHoraAberturaOS.Value).TotalHours,
                                                StatusSLAOS = v.StatusSLAOS ?? Constants.NENHUM_REGISTRO,
                                                StatusOS = v.StatusOS ?? Constants.NENHUM_REGISTRO,
                                                Intervencao = v.Intervencao ?? Constants.NENHUM_REGISTRO,
                                                Filial = v.Filial ?? Constants.NENHUM_REGISTRO,
                                                Autorizada = v.Autorizada ?? Constants.NENHUM_REGISTRO,
                                                Regiao = v.Regiao ?? Constants.NENHUM_REGISTRO,
                                                Cliente = v.Cliente ?? Constants.NENHUM_REGISTRO,
                                                NumBanco = v.NumBanco ?? Constants.NENHUM_REGISTRO,
                                                PA = v.PA.HasValue ? v.PA.Value.ToString() : Constants.NENHUM_REGISTRO,
                                                LocalAtendimento = v.LocalAtendimento ?? Constants.NENHUM_REGISTRO,
                                                Agencia = v.Agencia ?? Constants.NENHUM_REGISTRO,
                                                DCPosto = v.DCPosto ?? Constants.NENHUM_REGISTRO,
                                                Endereco = v.Endereco ?? Constants.NENHUM_REGISTRO,
                                                Bairro = v.Bairro ?? Constants.NENHUM_REGISTRO,
                                                Cidade = v.Cidade ?? Constants.NENHUM_REGISTRO,
                                                SiglaUF = v.SiglaUF ?? Constants.NENHUM_REGISTRO,
                                                Pais = v.Pais ?? Constants.NENHUM_REGISTRO,
                                                Contrato = v.NroContrato ?? Constants.NENHUM_REGISTRO,
                                                Equipamento = v.Equipamento ?? Constants.NENHUM_REGISTRO,
                                                NumSerie = v.NumSerie ?? Constants.NENHUM_REGISTRO,
                                                NumSerieCliente = v.NumSerieCliente ?? Constants.NENHUM_REGISTRO,
                                                Reincidencia = v.NumReincidencia.ToString() ?? Constants.NENHUM_REGISTRO,
                                                PontoEstrategico = v.PontoEstrategico ?? Constants.NENHUM_REGISTRO,
                                                SLA = v.SLA ?? Constants.NENHUM_REGISTRO,
                                                DefeitoRelatado = v.DefeitoRelatado ?? Constants.NENHUM_REGISTRO,
                                                ObservacaoCliente = v.ObservacaoCliente ?? Constants.NENHUM_REGISTRO,
                                                DataHoraCancelamento = v.DataHoraCancelamento.HasValue ? v.DataHoraCancelamento.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                MotivoCancelamento = v.MotivoCancelamento ?? Constants.NENHUM_REGISTRO,
                                                NumRAT = v.NumRAT ?? Constants.NENHUM_REGISTRO,
                                                StatusRAT = v.StatusRAT ?? Constants.NENHUM_REGISTRO,
                                                Tecnico = v.Tecnico ?? Constants.NENHUM_REGISTRO,
                                                RGTecnico = v.RGTecnico ?? Constants.NENHUM_REGISTRO,
                                                TipoServico = v.TipoServico ?? Constants.NENHUM_REGISTRO,
                                                DataHoraChegada = v.DataHoraChegada.HasValue ? v.DataHoraChegada.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                DataHoraInicio = v.DataHoraInicio.HasValue ? v.DataHoraInicio.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                DataHoraFim = v.DataHoraFim.HasValue ? v.DataHoraFim.Value.ToString("dd/MM/yy HH:mm") : Constants.NENHUM_REGISTRO,
                                                HorasTotalRAT = (v.DataHoraFim.HasValue && v.DataHoraInicio.HasValue) ?
                                                                v.DataHoraFim.Value.Subtract(v.DataHoraInicio.Value).TotalHours : 0,
                                                TempoAtendimentoMin = v.TempoAtendimentoMin.ToString() ?? Constants.NENHUM_REGISTRO,
                                                RelatoSolucao = v.RelatoSolucao ?? Constants.NENHUM_REGISTRO,
                                                ObsRAT = v.ObsRAT ?? Constants.NENHUM_REGISTRO,
                                                CodEDefeito = v.CodEDefeito ?? Constants.NENHUM_REGISTRO,
                                                NomeDefeito = v.NomeDefeito ?? Constants.NENHUM_REGISTRO,
                                                CodECausa = v.CodECausa ?? Constants.NENHUM_REGISTRO,
                                                NomeCausa = v.NomeCausa ?? Constants.NENHUM_REGISTRO,
                                                CodEAcao = v.CodEAcao ?? Constants.NENHUM_REGISTRO,
                                                NomeAcao = v.NomeAcao ?? Constants.NENHUM_REGISTRO,
                                                CodMagnus = v.CodMagnus ?? Constants.NENHUM_REGISTRO,
                                                NomePeca = v.NomePeca ?? Constants.NENHUM_REGISTRO,
                                                QtdePecas = v.QtdePecas.HasValue ? v.QtdePecas.ToString() : Constants.NENHUM_REGISTRO,
                                                IndServico = v.IndServico.HasValue ? v.IndServico.ToString() : Constants.NENHUM_REGISTRO,

                                            });

            var wsOs = Workbook.Worksheets.Add("Chamados");
            wsOs.Cell(2, 1).Value = osSheet;
            WriteHeaders(osSheet.FirstOrDefault(), wsOs);

            if (ratSheet.Any())
            {
                var wsRat = Workbook.Worksheets.Add("RATs");
                wsRat.Cell(2, 1).Value = ratSheet;
                WriteHeaders(ratSheet.FirstOrDefault(), wsRat);

                var wsRatDetalhe = Workbook.Worksheets.Add("Detalhes");
                wsRatDetalhe.Cell(2, 1).Value = ratDetalheSheet;
                WriteHeaders(ratDetalheSheet.FirstOrDefault(), wsRatDetalhe);

                var wsRatDetalhePeca = Workbook.Worksheets.Add("Peças");
                wsRatDetalhePeca.Cell(2, 1).Value = ratDetalhePecaSheet;
                WriteHeaders(ratDetalhePecaSheet.FirstOrDefault(), wsRatDetalhePeca);
            }

            var wsUni = Workbook.Worksheets.Add("Unificado");
            wsUni.Cell(2, 1).Value = exportUnificado;
            WriteHeaders(exportUnificado.FirstOrDefault(), wsUni);
        }
        
        private IActionResult GerarPdfOrdemServico(Exportacao exportacao)
        {

            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrdemServicoParameters>();
            var os = _osRepo.ObterPorCodigo(Int32.Parse(parameters.CodOS));
            var osImpressao = new OrdemServicoPdfHelper(os);
            var osPdf = GenerateFilePath($"OS-{os.CodOS}.pdf");
            
            osImpressao.GeneratePdf(osPdf);

            if (exportacao.Email != null)
            {
                arquivos.Add(osPdf);
                exportacao.Email.Anexos = arquivos;

                _emaiLService.Enviar(exportacao.Email);
                return new NoContentResult();
            }

            byte[] file = File.ReadAllBytes(osPdf);
            return new FileContentResult(file, "application/pdf");
        }

        private IActionResult GerarPdfOrdemServicoResumido(Exportacao exportacao)
        {

            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrdemServicoParameters>();
            

            using (WebClient wc = new())
            {
                var pathHTML = GenerateFilePath($"OS-{parameters.CodOS}.html");
                var pathPDF = GenerateFilePath($"OS-{parameters.CodOS}.pdf");
                var url = string.Format(@"https://sat.perto.com.br/prjSATWebOLD/ImprimeOS.asp?CodOs={0}", parameters.CodOS);
                byte[] data = wc.DownloadData(url);

                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertUrl(string.Format(pathHTML, parameters.CodOS, "html"));
                doc.Save(string.Format(pathPDF, parameters.CodOS, "pdf"));
                doc.Close();

                if (exportacao.Email != null)
                {
                    arquivos.Add(pathPDF);
                    exportacao.Email.Anexos = arquivos;

                    _emaiLService.Enviar(exportacao.Email);
                    return new NoContentResult();
                }

                byte[] file = File.ReadAllBytes(pathPDF.Replace("html", "pdf"));
                return new FileContentResult(file, "application/pdf");
            }
        }
    }
}
