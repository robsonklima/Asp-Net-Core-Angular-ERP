using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaInstalacao(InstalacaoParameters parameters)
        {
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);
            var sheet = instalacoes.Select(i => 
                            new
                            {
                                DtPagtoInstalacao = i.DtPagtoInstalacao,
                                VlrPagtoInstalacao = i.VlrPagtoInstalacao,
                                Bordero = i.Bordero,
                                Codigo = i.CodInstalacao,
                                Filial = i.Filial?.NomeFilial,
                                Autorizada = i.Autorizada?.NomeFantasia,
                                Regiao = i.Regiao?.NomeRegiao,
                                LoteImportacao = i.InstalacaoLote?.NomeLote,
                                DataRemessa = i.DataNFRemessa,
                                Contrato = i.Contrato?.NroContrato,
                                PedidoCompra = i.PedidoCompra,
                                SuperE = i.SuperE,
                                CMA = i.Csl,
                                Lote = i.CSOServ,
                                Terceirizado = i.Supridora,
                                TipoDep = i.MST606TipoNovo,
                                TipoNovo = i.Equipamento?.NomeEquip,
                                NSerie = i.EquipamentoContrato?.NumSerie,
                                NumeroTAANovo = i.EquipamentoContrato?.NumSerieCliente,
                                PrefixoSB = i.LocalAtendimentoSol?.NumAgencia?.Length > 0 ? $"{i.LocalAtendimentoSol?.NumAgencia}/{i.LocalAtendimentoSol?.DCPosto}" : Constants.NENHUM_REGISTRO,
                                Dependencia = i.LocalAtendimentoSol?.NomeLocal,
                                CPNJ = i.LocalAtendimentoIns?.Cnpj,
                                Logradouro = i.LocalAtendimentoIns?.Endereco,
                                Municipio = i.LocalAtendimentoSol?.Cidade?.NomeCidade,
                                UF = i.LocalAtendimentoSol?.Cidade?.UnidadeFederativa?.SiglaUF,
                                CEP = i.LocalAtendimentoIns?.Cep,
                                PrevisaoEntrega = i.DataSugEntrega,
                                DtConfirmadaEntrega = i.DataConfEntrega,
                                NFRemessa = i.NFRemessa,
                                NFRemessaDataExpedicao = i.DataNFRemessa,
                                DtExpedicao = i.DataExpedicao,
                                TransportadoraEntrega = i.Transportadora?.NomeTransportadora,
                                AgEntrega = i.LocalAtendimentoEnt?.NumAgencia?.Length > 0 ? $"{i.LocalAtendimentoEnt?.NumAgencia}/{i.LocalAtendimentoEnt?.DCPosto}" : Constants.NENHUM_REGISTRO,
                                NomeLocalEntrega = i.LocalAtendimentoEnt?.NomeLocal,
                                RecebimentoDocumentacaoInstalacao = i.DtbCliente,
                                FaturaTranspReEntrega = i.FaturaTranspReEntrega,
                                DtReEntrega = i.DtReEntrega,
                                ResponsavelRecebReEntrega = i.ResponsavelRecebReEntrega,
                                DataEntregaConfirmacao = i.DataHoraChegTranspBt,
                                ResponsavelRecebimento = i.NomeRespBancoBT,
                                MatResponsavelRecebimento = i.NumMatriculaBT,
                                BorderoTranspRecebido = i.IndBTOrigEnt == 1 ? "SIM" : "NÃO",
                                BorderoTranspConferido = i.IndBTOK == 1 ? "SIM" : "NÃO",
                                NFRemessaConferid = i.NFRemessaConferida,
                                PrevisaoInstalacao = i.DataSugInstalacao,
                                DtAgendadaInstalacao = i.DataConfInstalacao,
                                OS = i.OrdemServico?.CodOS,
                                DataHoraOS = i.OrdemServico?.DataHoraAberturaOS,
                                StatusOS = i.OrdemServico?.StatusServico?.NomeStatusServico,
                                RAT = i.OrdemServico?.RelatoriosAtendimento?.OrderByDescending(r => r.CodRAT)?.FirstOrDefault()?.NumRAT,
                                AgInstalacao = i.LocalAtendimentoIns?.NumAgencia?.Length > 0 ? $"{i.LocalAtendimentoIns?.NumAgencia}/{i.LocalAtendimentoIns?.DCPosto}" : Constants.NENHUM_REGISTRO,
                                NomeLocalInstalacao = i.LocalAtendimentoIns?.NomeLocal,
                                DtInstalacao = i.DataBI,
                                QtdParabold = i.QtdParaboldBI,
                                EquipamentoRebaixado = i.IndEquipRebaixadoBI == 1 ? "SIM" : "NÃO",
                                ResponsavelInstalacaoBanco = i.NomeRespBancoBI,
                                MatResponsavelInstalacaoBanco = i.NumMatriculaBI,
                                TermoAceite = i.IndBiorigEnt == 1 ? "SIM" : "NÃO",
                                TermoDescaracterizacao = i.TermoDescaracterizacao,
                                Laudo = i.IndLaudoOK == 1 ? "SIM" : "NÃO",
                                RE5330 = i.IndRE5330OK == 1 ? "SIM" : "NÃO",
                                RATEntregue = i.IndRATOK == 1 ? "SIM" : "NÃO",
                                FornecedorTradeIn1 = i.FornecedorTradeIn1,
                                FornecedorTradeIn2 = i.FornecedorTradeIn2,
                                NumBemTradeIn = i.BemTradeIn,
                                FornecedorBemTradeIn = i.Fabricante,
                                TipoBemTradeIn = i.Modelo,
                                NFTradeIn1 = i.NFTradeIn1,
                                NFTradeIn2 = i.NFTradeIn2,
                                DataNFTradeIn1 = i.DataNFTradeIn1,
                                DataNFTradeIn2 = i.DataNFTradeIn2,
                                ValorTradeIn1 = i.VlrDesFixacao1,
                                ValorTradeIn2 = i.VlrDesFixacao2,
                                ValorKmTradeIn1 = i.vlrKMTradeIn1,
                                ValorKmTradeIn2 = i.vlrKMTradeIn2,
                                DistanciaKmTradeIn1 = i.DistanciaKmTradeIn1,
                                DistanciaKmTradeIn2 = i.DistanciaKmTradeIn2,
                                ValorTotalTradeIn1 = ((i.VlrDesFixacao1 + i.vlrKMTradeIn1) * i.DistanciaKmTradeIn1),
                                ValorTotalTradeIn2 = ((i.VlrDesFixacao2 + i.vlrKMTradeIn2) * i.DistanciaKmTradeIn2),
                                TransportadoraTradeIn = i.NomeTransportadora,
                                DtPrevRecolhimentoTradeIn = i.DtPrevRecolhimentoTradeIn,
                                DataRetiradaBemTradeIn = i.DataRetirada,
                                Romaneio = i.Romaneio,
                                NFTranspTradeIn = i.NFTransportadoraTradeIn,
                                DataNFTransTradeIn = i.DataNFTransportadoraTradeIn,
                                ValorRecolhimentoTradeIn = i.VlrRecolhimentoTradeIn,
                                ReponsavelBancoAcompanhamentoTradeIn = i.NomeReponsavelBancoAcompanhamento,
                                MatriculaRespBancoAcompanhamentoRecolhimento = i.NumMatriculaRespBancoAcompanhamento,
                                FornecedorCompraTradeIn = i.FornecedorCompraTradeIn,
                                NFVendaTradeIn = i.NFVendaTradeIn,
                                DtNFVendaTradeIn = i.DataNFVendaTradeIn,
                                ValorVendaTradeIn = i.ValorUnitarioVenda,
                                NFInstalacaoAutorizada = i.InstalacaoNFAut?.NFAut,
                                DtNFInstalacaoAutorizada = i.InstalacaoNFAut?.DataNFAut,
                                NFVenda = i.InstalacaoNFVenda?.NumNFVenda,
                                NFVendaDataEmissao = i.InstalacaoNFVenda?.DataNFVenda,
                                DtEnvioNFVenda = i.InstalacaoNFVenda?.DataNFVendaEnvioCliente,
                                DtRecebimentoNFVenda = i.InstalacaoNFVenda?.DataNFVendaRecebimentoCliente,
                                //DtPagtoInstalacao = _instalPagtoIntalRepo.ObterPorParametros(new InstalacaoPagtoInstalParameters { CodInstalacao = i.CodInstalacao }).OrderByDescending(i => i.CodInstalPagto)?.FirstOrDefault()?.DataHoraCad,
                                //VlrPagtoInstalacao = _instalPagtoIntalRepo.ObterPorParametros(new InstalacaoPagtoInstalParameters { CodInstalacao = i.CodInstalacao }).OrderByDescending(i => i.CodInstalPagto)?.FirstOrDefault()?.VlrParcela,
                                //DtPagtoInstalacao = i.DtPagtoInstalacao,
                                //VlrPagtoInstalacao = i.VlrPagtoInstalacao,
                                //Bordero = i.Bordero,
                                DTVencimentoBordero100Perc = i.DTVencBord100,
                                DTEntregaBordero100Perc = i.DTEntBord100,
                                DTVencimentoBordero90Perc = i.DTVencBord90,
                                DTEntregaBordero90Perc = i.DTEntBord90,
                                DTVencimentoBordero10Perc = i.DTVencBord10,
                                DTEntregaBordero10Perc = i.DTEntBord10,
                                ValorFrete1 = i.ValorFrete1,
                                FaturaFrete1 = i.FaturaFrete1,
                                CTEFrete1 = i.CteFrete1,
                                DataFaturaFrete1 = i.DTFaturaFrete1,
                                ValorFrete2 = i.ValorFrete2,
                                FaturaFrete2 = i.FaturaFrete2,
                                CTEFrete2 = i.CteFrete2,
                                DataFaturaFrete2 = i.DTFaturaFrete2,
                                ValorExtraFrete = i.ValorExtraFrete,
                                DDD = i.Ddd,
                                Telefone1 = i.Telefone1,
                                Redestinacao = i.Redestinacao,
                                AntigoPrefixoRedestinacao = i.AntigoPrefixoRedestinacao,
                                AntigoSBRedestinacao = i.AntigoSbRedestinacao,
                                AntigoNomeDependenciaRedestinacao = i.AntigoNomeDependenciaRedestinacao,
                                AntigoUFRedestinacao = i.AntigoUfRedestinacao,
                                AntigoTipoDependenciaRedestinacao = i.AntigoTipoDependenciaRedestinacao,
                                AntigoPedidoCompraRedestinacao = i.AntigoPedidoCompraRedestinacao,
                                AntigoProtocoloCDO = i.AntigoProtocoloCdo,
                                NovoProtocoloCDO = i.NovoProtocoloCdo,
                            });


            // InstalacaoPleitoInstal bordero = _instalacaoPleitoInstalRepo.ObterPorParametros(new InstalacaoPleitoInstalParameters { CodInstalacao = i.CodInstalacao })?.FirstOrDefault();

            // if(bordero?.CodInstalacao != null)
            //     i.Bordero = bordero.CodInstalPleito;

            // InstalacaoPagtoInstal pagamento = _instalPagtoIntalRepo
            //     .ObterPorParametros(new InstalacaoPagtoInstalParameters { CodInstalacao = i.CodInstalacao })
            //         .OrderByDescending(p => p.CodInstalPagto)?.FirstOrDefault();

            // if(pagamento?.CodInstalacao  != null){
            //     i.DtPagtoInstalacao = pagamento.DataHoraCad;
            //     i.VlrPagtoInstalacao = pagamento.VlrParcela;

            var wsOs = Workbook.Worksheets.Add("Instalacoes");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}