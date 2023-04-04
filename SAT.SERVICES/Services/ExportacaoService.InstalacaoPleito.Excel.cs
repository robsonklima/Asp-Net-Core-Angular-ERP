using System.Linq;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaInstalacaoPleito(InstalacaoPleitoParameters parameters)
        {
            var instalacoesPleito = _instalacaoPleitoRepo.ObterPorParametros(parameters);
            var instalacoes = _instalacaoRepo.ObterPorParametros(new InstalacaoParameters { CodContrato = parameters.CodContrato, CodEquips = parameters.CodEquips, CodInstalacoes = parameters.CodInstalacoes });
            var sheet = instalacoes.Select(i =>
                            new
                            {
                                Lote = i.InstalacaoLote?.NomeLote,
                                Contrato = i.EquipamentoContrato?.Contrato?.NomeContrato,
                                NFVenda = i.InstalacaoNFVenda?.NumNFVenda,
                                NFRemessa = i.NFRemessa,
                                Prefixo = i.LocalAtendimentoIns?.NumAgencia,
                                Posto = i.LocalAtendimentoIns?.DCPosto,
                                Agencia = i.LocalAtendimentoIns?.NomeLocal,
                                Endereço = i.LocalAtendimentoIns?.Endereco,
                                Cidade = i.LocalAtendimentoIns?.Cidade?.NomeCidade,
                                UF = i.LocalAtendimentoIns?.Cidade?.UnidadeFederativa?.SiglaUF,
                                Filial = i.Filial?.NomeFilial,
                                Equipamento = i.Equipamento?.NomeEquip,
                                NumSerie = i.EquipamentoContrato?.NumSerie,
                                BemTradeIn = i.BemTradeIn,
                                PedidoCompra = i.PedidoCompra,  
                                ValorInstalacao = i.Contrato?.ContratosEquipamento?
                                                    .Where(ce => ce?.CodEquip == i.CodEquip)
                                                    .Sum(ce => ce.VlrInstalacao),
                                ValorUnitario = i.Contrato?.ContratosEquipamento?
                                                    .Where(ce => ce?.CodEquip == i.CodEquip)
                                                    .Sum(ce => ce.VlrUnitario)
                            });

            var wsOs = Workbook.Worksheets.Add("instalacoesPleito");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}