using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaInstalacao(InstalacaoParameters parameters)
		{
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);
            var sheet = instalacoes.Select(i =>
                            new
                            {
                                Codigo = i.CodInstalacao,
                                Filial =  i.Filial.NomeFilial,
                                Autorizada = i.Autorizada?.NomeFantasia,
                                Regiao = i.Regiao.NomeRegiao,
                                LoteImportacao = i.InstalacaoLote.NomeLote,
                                DataRemessa = i.DataNFRemessa,
                                Contrato = i.Contrato.NroContrato,
                                PedidoCompra = i.PedidoCompra,
                                SuperE = i.SuperE,
                                CMA = i.Csl,
                                Lote = i.CSOServ,
                                Terceirizado = i.Supridora,
                                TipoDep = i.MST606TipoNovo,
                                TipoNovo = i.Equipamento.NomeEquip,
                                NSerie = i.EquipamentoContrato?.NumSerie,
                                NumeroTAANovo = i.EquipamentoContrato?.NumSerieCliente,
                                PrefixoSB = $"{i.LocalAtendimentoSol?.NumAgencia}/{i.LocalAtendimentoSol?.DCPosto}",
                                Dependencia = i.AntigoNomeDependenciaRedestinacao,
                                CPNJ = i.LocalAtendimentoIns?.Cnpj,
                                Logradouro = i.LocalAtendimentoIns?.Endereco,
                                Municipio = i.LocalAtendimentoIns?.Cidade?.NomeCidade,
                                UF = i.LocalAtendimentoIns?.Cidade?.UnidadeFederativa?.SiglaUF,
                                CEP = i.LocalAtendimentoIns?.Cep,
                                PrevisaoEntrega = i.DataSugEntrega,
                                DtConfirmadaEntrega = i.DataConfEntrega,
                                NFRemessa = i.NFRemessa,
                                NFRemessaDataExpedicao = i.DataNFRemessa,
                                DtExpedicao = i.DataExpedicao,
                                TransportadoraEntrega = i.Transportadora?.NomeTransportadora,
                                AgEntrega = $"{i.LocalAtendimentoEnt?.NumAgencia}/{i.LocalAtendimentoEnt?.DCPosto}",
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
                                AgInstalacao = $"{i.LocalAtendimentoIns?.NumAgencia}/{i.LocalAtendimentoIns?.DCPosto}",
                                NomeLocalInstalacao = i.LocalAtendimentoIns?.NomeLocal,
                                DtInstalacao = i.DataBI,
                                QtdParabold = i.QtdParaboldBI,
                                EquipamentoRebaixado = i.IndEquipRebaixadoBI
                            });

            var wsOs = Workbook.Worksheets.Add("Instalacoes");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}