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
            instalacaoPleito.Contrato.Cliente = _clienteRepo.ObterPorCodigo(instalacaoPleito.Contrato.CodCliente);
            var instalacoes = _instalacaoRepo.ObterPorParametros(new InstalacaoParameters { CodContrato = instalacaoPleito.CodContrato, CodEquips = parameters.CodEquips, CodInstalacoes = parameters.CodInstalacoes });
            var impressao = new InstalacaoPleitoPdfHelper(instalacaoPleito, instalacoes);
            var pdf = GenerateFilePath($"INSTALACAO-PLEITO-{instalacaoPleito.CodInstalPleito}.pdf");
            impressao.GeneratePdf(pdf);
            arquivos.Add(pdf);
            byte[] file = File.ReadAllBytes(pdf);

            return new FileContentResult(file, "application/pdf");
        }
    }
}

