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
        private IActionResult GerarPdfLaudo(Exportacao exportacao)
        {
            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<LaudoParameters>();
            var laudo = _laudoRepo.ObterPorCodigo(parameters.CodLaudo.Value);
            var os = _osRepo.ObterPorCodigo(laudo.CodOS);
            var laudoImpressao = new LaudoPdfHelper(os, laudo);
            var laudoPdf = GenerateFilePath($"LAUDO-{laudo.CodOS}.pdf");
            laudoImpressao.GeneratePdf(laudoPdf);
            arquivos.Add(laudoPdf);

            byte[] file = File.ReadAllBytes(laudoPdf);

            return new FileContentResult(file, "application/pdf");
        }
    }
}

