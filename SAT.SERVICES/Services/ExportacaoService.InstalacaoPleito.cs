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
        private IActionResult GerarPdfInstalacaoPleito(Exportacao exportacao)
        {
            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<InstalacaoPleitoParameters>();
            var instalacaoPleito = _instalacaoPleitoRepo.ObterPorCodigo(parameters.CodInstalPleito.Value);
            var impressao = new InstalacaoPleitoPdfHelper(instalacaoPleito);
            var pdf = GenerateFilePath($"INSTALACAO-PLEITO-{instalacaoPleito.CodInstalPleito}.pdf");
            impressao.GeneratePdf(pdf);
            arquivos.Add(pdf);
            byte[] file = File.ReadAllBytes(pdf);

            return new FileContentResult(file, "application/pdf");
        }
    }
}

