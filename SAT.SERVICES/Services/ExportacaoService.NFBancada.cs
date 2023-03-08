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
        private IActionResult GerarPdfNFBancada(Exportacao exportacao)
        {
            var arquivos = new List<string>();

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OSBancadaPecasParameters>();
            var osBancadaPecas = _osBancadaPecasRepo.ObterPorCodigo(parameters.CodOsbancada.Value,parameters.CodPecaRe5114.Value);
            var impressao = new NFBancadaPdfHelper(osBancadaPecas);
            var pdf = GenerateFilePath($"NF-BANCADA-{osBancadaPecas.OSBancada.Nfentrada}.pdf");
            impressao.GeneratePdf(pdf);
            arquivos.Add(pdf);
            byte[] file = File.ReadAllBytes(pdf);

            return new FileContentResult(file, "application/pdf");
        }
    }
}

