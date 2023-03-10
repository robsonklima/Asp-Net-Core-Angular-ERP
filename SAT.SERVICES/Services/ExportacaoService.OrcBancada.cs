using System.Collections.Generic;
using System.IO;
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
        private IActionResult GerarPdfOrcBancada(Exportacao exportacao)
        {
            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OsBancadaPecasOrcamentoParameters>();
            var orcBancada = _osBancadaPecasOrcamentoRepo.ObterPorCodigo(parameters.CodOrcamento.Value);
            var pecasEspec = _orcamentoPecasEspecRepo.ObterPorParametros(new OrcamentoPecasEspecParameters {
                CodOrcamento = orcBancada.CodOrcamento
            });
            var impressao = new OrcBancadaPdfHelper(orcBancada, pecasEspec);
            var pdf = GenerateFilePath($"ORCAMENTO-BANCADA-{orcBancada.CodOrcamento}.pdf");
            impressao.GeneratePdf(pdf);
            arquivos.Add(pdf);
            byte[] file = File.ReadAllBytes(pdf);

            return new FileContentResult(file, "application/pdf");
        }
    }
}

