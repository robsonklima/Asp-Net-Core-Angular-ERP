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

            var usuario = _usuarioService.ObterPorCodigo(_contextAcecssor.HttpContext.User.Identity.Name);

            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OSBancadaPecasParameters>();
            var osBancadaPecas = _osBancadaPecasRepo.ObterPorCodigo(parameters.CodOsbancada.Value,parameters.CodPecaRe5114.Value);
            var pecas = _osBancadaPecasRepo.ObterPorParametros(new OSBancadaPecasParameters {
                CodOsbancada = parameters.CodOsbancada.Value,
                IndImpressao = 0,
                IndPecaDevolvida = 1
            });
            var impressao = new NFBancadaPdfHelper(osBancadaPecas, pecas, usuario);
            var pdf = GenerateFilePath($"NF-BANCADA-{osBancadaPecas.OSBancada.Nfentrada}.pdf");
            impressao.GeneratePdf(pdf);
            arquivos.Add(pdf);
            byte[] file = File.ReadAllBytes(pdf);
            
            pecas.ForEach(delegate(OSBancadaPecas peca)
            {
                peca.IndImpressao = 1;
                _osBancadaPecasRepo.Atualizar(peca);
            });

            return new FileContentResult(file, "application/pdf");
        }
    }
}

