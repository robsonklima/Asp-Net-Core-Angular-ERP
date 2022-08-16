using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestPDF.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaOrcamento(OrcamentoParameters parameters)
        {
            var orcamentos = _orcamentoRepo.ObterPorParametros(parameters);
            var sheet = orcamentos.Select(orcamento =>
                            new
                            {
                                Numero = orcamento.Numero,
                            });

            var wsOs = Workbook.Worksheets.Add("orcamentos");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }

        private IActionResult GerarPdfOrcamento(Exportacao exportacao)
        {

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrcamentoParameters>();
            
            var orcamento = _orcamentoRepo.ObterPorCodigo(parameters.CodigoOrdemServico.Value);
            var orcamentoImpressao = new OrcamentoPdfHelper(orcamento);
            var pdfFile = GenerateFilePath($"ORÇAMENTO-{orcamento.Numero}.pdf");
            orcamentoImpressao.GeneratePdf(pdfFile);

            if (exportacao.Email != null)
            {  
                exportacao.Email.PathAnexo = pdfFile;

                new EmailService().Enviar(exportacao.Email);

                return new NoContentResult();
            }

            byte[] file = File.ReadAllBytes(pdfFile);
            return new FileContentResult(file, "application/pdf");
        }
    }
}