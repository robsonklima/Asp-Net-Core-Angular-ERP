using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QuestPDF.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOrcamento(OrcamentoParameters parameters)
        {
            var orcamentos = _orcamentoRepo.ObterPorView(parameters);
            var sheet = orcamentos.Select(orcamento => new {
                Numero = orcamento.Numero,
                CodOrc = orcamento.CodOrc,
                DataOrcamento = orcamento.DataOrcamento,
                CodOS = orcamento.CodOS,
                DataAbertura = orcamento.DataAbertura,
                StatusOS = orcamento.StatusOS,
                StatusLaudo = orcamento.StatusLaudo,
                NomeLocal = orcamento.NomeLocal,
                Intervencao = orcamento.Intervencao,
                Autorizada = orcamento.Autorizada,
                Regiao = orcamento.Regiao,
                Cliente = orcamento.Cliente,
                Equipamento = orcamento.Equipamento,
                NumSerie = orcamento.NumSerie,
                NumOSCliente = orcamento.NumOSCliente
            });

            var wsOs = Workbook.Worksheets.Add("orcamentos");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }

        private IActionResult GerarPdfOrcamento(Exportacao exportacao)
        {
            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrcamentoParameters>();
            var orcamento = _orcamentoRepo.ObterPorCodigo(parameters.codOrc.Value);
            orcamento.OrdemServico.Fotos = _fotoRepo.ObterPorParametros(new FotoParameters {
                CodOS = orcamento.CodigoOrdemServico
            });

            var orcamentoImpressao = new OrcamentoPdfHelper(orcamento);
            var orcamentoPdf = GenerateFilePath($"ORÇAMENTO-{orcamento.Numero}.pdf");
            
            orcamentoImpressao.GeneratePdf(orcamentoPdf);
            arquivos.Add(orcamentoPdf);

            if (parameters.IncluirLaudoExportacao)
            {
                var laudoImpressao = new LaudoPdfHelper(orcamento.OrdemServico);
                var laudoPdf = GenerateFilePath($"LAUDO-{orcamento.Numero}.pdf");
                laudoImpressao.GeneratePdf(laudoPdf);
                arquivos.Add(laudoPdf);
            }

            if (exportacao.Email != null)
            {
                exportacao.Email.Anexos = arquivos;

                _emaiLService.Enviar(exportacao.Email);
                return new NoContentResult();
            }

            byte[] file = File.ReadAllBytes(orcamentoPdf);
            
            return new FileContentResult(file, "application/pdf");
        }
    }
}