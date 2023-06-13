using System;
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
            var laudoImpressao = new LaudoPdfHelper(os,laudo);
            var laudoPdf = GenerateFilePath($"LAUDO-{laudo.CodOS}.pdf");
            laudoImpressao.GeneratePdf(laudoPdf);
            arquivos.Add(laudoPdf);

            byte[] file = File.ReadAllBytes(laudoPdf);

            return new FileContentResult(file, "application/pdf");
        }

        private IActionResult GerarZipLaudo(Exportacao exportacao)
        {
            var user = _contextAcecssor.HttpContext.User.Identity.Name;
            var prefixo = DateTime.Now.ToString("yyyyMMddHHmmss" + user);
            var path = GenerateFilePath("");
            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrdemServicoParameters>();
            var ordens = _osRepo.ObterPorParametros(parameters);

            foreach (var ordem in ordens)
            {
                foreach (var rat in ordem.RelatoriosAtendimento)
                {
                    foreach (var laudo in rat.Laudos)
                    {
                        var l = _laudoRepo.ObterPorCodigo(laudo.CodLaudo);
                        var laudoImpressao = new LaudoPdfHelper(ordem,l);
                        var laudoPdf = $"{path}/LAUDO-{ordem.CodOS}-{prefixo}.pdf";
                        laudoImpressao.GeneratePdf(laudoPdf);
                    }
                }
            }

            GenericHelper.CompressDirectory(
                path,
                path + $"exportacao-{user}.zip",
                9,
                $"{prefixo}.pdf"
            );

            byte[] file = File.ReadAllBytes(path + $"exportacao-{user}.zip");
            return new FileContentResult(file, "application/zip");
        }
    }
}

