using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        protected void GerarPlanilhaInstalacao(InstalacaoParameters parameters)
        {
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);
            var viewUnificada = _instalacaoRepo.ObterViewPorInstalacao(instalacoes.Select(instalacao => instalacao.CodInstalacao).ToArray());

            var sheet = viewUnificada.Select(v =>
                            new
                            {
                                CodInstalacao = v.CodInstalacao.HasValue ? v.CodInstalacao.Value.ToString() : " ",
                                Filial = v.Filial,
                                Autorizada = v.Autorizada,
                                Regiao = v.Regiao,
                                LoteImportacao = v.LoteImportacao,
                                DataRemessa = v.DataRemessa.HasValue ? v.DataRemessa.Value.ToString("dd/MM/yy") : " ",
                                Contrato = v.Contrato,
                                PedidoCompra = v.PedidoCompra,
                                SuperE = v.SuperE,
                                CMA = v.CMA,
                                Lote = v.Lote,
                                Terceirizado = v.Terceirizado,
                                TipoDep = v.TipoDep,
                                TipoNovo = v.TipoNovo,
                                NSerie = v.NSerie,
                                NumeroTAANovo = v.NumeroTAANovo,
                                PrefixoSB = v.PrefixoSB,
                                Dependencia = v.Dependencia,
                                CNPJ = v.CNPJ,
                                Logradouro = v.Logradouro,
                                Municipio = v.Municipio,
                                UF = v.UF,
                                CEP = v.CEP,
                                PrevisaoEntrega = v.PrevisaoEntrega.HasValue ? v.PrevisaoEntrega.Value.ToString("dd/MM/yy") : " ",
                                DataEntregaConfirmacao = v.DataEntregaConfirmacao.HasValue ? v.DataEntregaConfirmacao.Value.ToString("dd/MM/yy") : " ",
                                NFRemessa = v.NFRemessa,
                                NFRemessaDataExpedicao = v.NFRemessaDataExpedicao.HasValue ? v.NFRemessaDataExpedicao.Value.ToString("dd/MM/yy") : " ",
                                DtExpedicao = v.DtExpedicao.HasValue ? v.DtExpedicao.Value.ToString("dd/MM/yy") : " ",
                                TransportadoraEntrega = v.TransportadoraEntrega,
                                AgEntrega = v.AgEntrega,
                                NomeLocalEntrega = v.NomeLocalEntrega,
                                RecebimentoDocumentacaoInstalacao = v.RecebimentoDocumentacaoInstalacao,
                                FaturaTranspReEntrega = v.FaturaTranspReEntrega,
                                DtReEntrega = v.DtReEntrega.HasValue ? v.DtReEntrega.Value.ToString("dd/MM/yy") : " ",
                                ResponsavelRecebReEntrega = v.ResponsavelRecebReEntrega,
                                ResponsavelRecebimento = v.ResponsavelRecebimento,
                                MatResponsavelRecebimento = v.MatResponsavelRecebimento,
                                BorderoTranspRecebido = v.BorderoTranspRecebido,
                                BorderoTranspConferido = v.BorderoTranspConferido,
                                NFRemessaConferida = v.NFRemessaConferida,
                                PrevisaoInstalacao = v.PrevisaoInstalacao.HasValue ? v.PrevisaoInstalacao.Value.ToString("dd/MM/yy") : " ",
                                DtAgendadaInstalacao = v.DtAgendadaInstalacao.HasValue ? v.DtAgendadaInstalacao.Value.ToString("dd/MM/yy") : " ",
                                OS = v.OS.HasValue ? v.OS.Value.ToString() : " ",
                                DataHoraOS = v.DataHoraOS.HasValue ? v.DataHoraOS.Value.ToString("dd/MM/yy") : " ",
                                StatusOS = v.StatusOS,
                                RAT = v.RAT,
                                AgInstalacao = v.AgInstalacao,
                                NomeLocalInstalacao = v.NomeLocalInstalacao,
                                DtInstalacao = v.DtInstalacao.HasValue ? v.DtInstalacao.Value.ToString("dd/MM/yy") : " ",
                                QtdParabold = v.QtdParabold.HasValue ? v.QtdParabold.Value.ToString() : " ",
                                EquipamentoRebaixado = v.EquipamentoRebaixado,
                                ResponsavelInstalacaoBanco = v.ResponsavelInstalacaoBanco,
                                MatResponsavelInstalacaoBanco = v.MatResponsavelInstalacaoBanco,
                                TermoAceite = v.TermoAceite,
                                TermoDescaracterizacao = v.TermoDescaracterizacao.HasValue ? v.TermoDescaracterizacao.Value.ToString() : " ",
                                Laudo = v.Laudo,
                                RE5330 = v.RE5330,
                                RATEntregue = v.RATEntregue,
                                FornecedorTradeIn1 = v.FornecedorTradeIn1,
                                FornecedorTradeIn2 = v.FornecedorTradeIn2,
                                NumBemTradeIn = v.NumBemTradeIn,
                                FornecedorBemTradeIn = v.FornecedorBemTradeIn,
                                TipoBemTradeIn = v.TipoBemTradeIn,
                                NFTradeIn1 = v.NFTradeIn1,
                                NFTradeIn2 = v.NFTradeIn2,
                                DataNFTradeIn1 = v.DataNFTradeIn1.HasValue ? v.DataNFTradeIn1.Value.ToString("dd/MM/yy") : " ",
                                DataNFTradeIn2 = v.DataNFTradeIn2.HasValue ? v.DataNFTradeIn2.Value.ToString("dd/MM/yy") : " ",
                                ValorTradeIn1 = string.Format("{0:C}", v.ValorTradeIn1),
                                ValorTradeIn2 = string.Format("{0:C}", v.ValorTradeIn2),
                                ValorKmTradeIn1 = string.Format("{0:C}", v.ValorKmTradeIn1),
                                ValorKmTradeIn2 = string.Format("{0:C}", v.ValorKmTradeIn2),
                                DistanciaKmTradeIn1 = string.Format("{0:C}", v.DistanciaKmTradeIn1),
                                DistanciaKmTradeIn2 = string.Format("{0:C}", v.DistanciaKmTradeIn2),
                                ValorTotalTradeIn1 = string.Format("{0:C}", v.ValorTotalTradeIn1),
                                ValorTotalTradeIn2 = string.Format("{0:C}", v.ValorTotalTradeIn2),
                                TransportadoraTradeIn = v.TransportadoraTradeIn,
                                DtPrevRecolhimentoTradeIn = v.DtPrevRecolhimentoTradeIn.HasValue ? v.DtPrevRecolhimentoTradeIn.Value.ToString("dd/MM/yy") : " ",
                                DataRetiradaBemTradeIn = v.DataRetiradaBemTradeIn.HasValue ? v.DataRetiradaBemTradeIn.Value.ToString("dd/MM/yy") : " ",
                                Romaneio = v.Romaneio,
                                NFTranspTradeIn = v.NFTranspTradeIn,
                                DataNFTransTradeIn = v.DataNFTransTradeIn.HasValue ? v.DataNFTransTradeIn.Value.ToString("dd/MM/yy") : " ",
                                ValorRecolhimentoTradeIn = string.Format("{0:C}", v.ValorRecolhimentoTradeIn),
                                ReponsavelBancoAcompanhamentoTradeIn = v.ReponsavelBancoAcompanhamentoTradeIn,
                                MatriculaRespBancoAcompanhamentoRecolhimento = v.MatriculaRespBancoAcompanhamentoRecolhimento,
                                FornecedorCompraTradeIn = v.FornecedorCompraTradeIn,
                                NFVendaTradeIn = v.NFVendaTradeIn,
                                DtNFVendaTradeIn = v.DtNFVendaTradeIn.HasValue ? v.DtNFVendaTradeIn.Value.ToString("dd/MM/yy") : " ",
                                ValorVendaTradeIn = string.Format("{0:C}", v.ValorVendaTradeIn),
                                NFInstalacaoAutorizada = v.NFInstalacaoAutorizada,
                                DtNFInstalacaoAutorizada = v.DtNFInstalacaoAutorizada.HasValue ? v.DtNFInstalacaoAutorizada.Value.ToString("dd/MM/yy") : " ",
                                NFVenda = v.NFVenda.HasValue ? v.NFVenda.Value.ToString() : " ",
                                NFVendaDataEmissao = v.NFVendaDataEmissao.HasValue ? v.NFVendaDataEmissao.Value.ToString("dd/MM/yy") : " ",
                                DtEnvioNFVenda = v.DtEnvioNFVenda.HasValue ? v.DtEnvioNFVenda.Value.ToString("dd/MM/yy") : " ",
                                DtRecebimentoNFVenda = v.DtRecebimentoNFVenda.HasValue ? v.DtRecebimentoNFVenda.Value.ToString("dd/MM/yy") : " ",
                                DtPagtoEntrega = v.DtPagtoEntrega.HasValue ? v.DtPagtoEntrega.Value.ToString("dd/MM/yy") : " ",
                                VlrPagtoEntrega = string.Format("{0:C}", v.VlrPagtoEntrega),                              
                                DtPagtoInstalacao = v.DtPagtoInstalacao.HasValue ? v.DtPagtoInstalacao.Value.ToString("dd/MM/yy") : " ",
                                VlrPagtoInstalacao = string.Format("{0:C}", v.VlrPagtoInstalacao),
                                Bordero = v.Bordero.HasValue ? v.Bordero.Value.ToString() : " ",
                                BorderoDesc = v.BorderoDesc,
                                DTVencimentoBordero100Perc = v.DTVencimentoBordero100Perc.HasValue ? v.DTVencimentoBordero100Perc.Value.ToString("dd/MM/yy") : " ",
                                DTEntregaBordero100Perc = v.DTEntregaBordero100Perc.HasValue ? v.DTEntregaBordero100Perc.Value.ToString("dd/MM/yy") : " ",
                                DTVencimentoBordero90Perc = v.DTVencimentoBordero90Perc.HasValue ? v.DTVencimentoBordero90Perc.Value.ToString("dd/MM/yy") : " ",
                                DTEntregaBordero90Perc = v.DTEntregaBordero90Perc.HasValue ? v.DTEntregaBordero90Perc.Value.ToString("dd/MM/yy") : " ",
                                DTVencimentoBordero10Perc = v.DTVencimentoBordero10Perc.HasValue ? v.DTVencimentoBordero10Perc.Value.ToString("dd/MM/yy") : " ",
                                DTEntregaBordero10Perc = v.DTEntregaBordero10Perc.HasValue ? v.DTEntregaBordero10Perc.Value.ToString("dd/MM/yy") : " ",
                                ValorFrete1 = string.Format("{0:C}", v.ValorFrete1),
                                FaturaFrete1 = v.FaturaFrete1,  
                                CteFrete1 = v.CteFrete1,
                                DataFaturaFrete1 = v.DataFaturaFrete1.HasValue ? v.DataFaturaFrete1.Value.ToString("dd/MM/yy") : " ",
                                ValorFrete2 = string.Format("{0:C}", v.ValorFrete2),
                                FaturaFrete2 = v.FaturaFrete2,
                                CteFrete2 = v.CteFrete2,
                                DataFaturaFrete2 = v.DataFaturaFrete2.HasValue ? v.DataFaturaFrete1.Value.ToString("dd/MM/yy") : " ",
                                ValorExtraFrete = string.Format("{0:C}", v.ValorExtraFrete),
                                DDD = v.DDD,
                                Telefone1 = v.Telefone1,
                                Redestinacao = v.Redestinacao,
                                AntigoPrefixoRedestinacao = v.AntigoPrefixoRedestinacao,
                                AntigoSbRedestinacao = v.AntigoSbRedestinacao,
                                AntigoNomeDependenciaRedestinacao = v.AntigoNomeDependenciaRedestinacao,
                                AntigoUfRedestinacao = v.AntigoUfRedestinacao,
                                AntigoTipoDependenciaRedestinacao = v.AntigoTipoDependenciaRedestinacao,
                                AntigoPedidoCompraRedestinacao = v.AntigoPedidoCompraRedestinacao,
                                AntigoProtocoloCdo = v.AntigoProtocoloCdo,
                                NovoProtocoloCdo = v.NovoProtocoloCdo,
                                DtLimitedaEntrega = v.DtLimitedaEntrega.HasValue ? v.DtLimitedaEntrega.Value.ToString("dd/MM/yy") : " ",
                            });

            var wsOs = Workbook.Worksheets.Add("Instalacoes");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }

        private IActionResult GerarZipTermosInstalacao(Exportacao exportacao)
        {
            var user = _contextAcecssor.HttpContext.User.Identity.Name;
            var prefixo = DateTime.Now.ToString("yyyyMMddHHmmss" + user);
            var path = GenerateFilePath("");
            var parameters = ((JObject)exportacao.EntityParameters).ToObject<OrdemServicoParameters>();
            var ordens = _osRepo.ObterPorParametros(parameters);

            foreach (var ordem in ordens)
            {
                var osImpressao = new OrdemServicoTermosPdfHelper(ordem);
                var osPdf = $"{path}/Termos-{ordem.CodOS}-{prefixo}.pdf";
                osImpressao.GeneratePdf(osPdf);
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