using System.Linq;
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
                                Bordero = instalacoesPleito[0].CodInstalPleito,
                                Lote = i.InstalacaoLote?.NomeLote,
                                Contrato = i.Contrato?.NomeContrato,
                                NFVenda = i.InstalacaoNFVenda?.NumNFVenda,
                                NFRemessa = i.NFRemessa,
                                Prefixo = i.EquipamentoContrato?.LocalAtendimento?.NumAgencia,
                                Posto = i.EquipamentoContrato?.LocalAtendimento?.DCPosto,
                                Agencia = i.EquipamentoContrato?.LocalAtendimento?.NomeLocal,
                                Endereço = i.EquipamentoContrato?.LocalAtendimento?.Endereco,
                                Cidade = i.EquipamentoContrato?.LocalAtendimento?.Cidade?.NomeCidade,
                                UF = i.EquipamentoContrato?.LocalAtendimento?.Cidade?.UnidadeFederativa?.SiglaUF,
                                Filial = i.Filial?.NomeFilial,
                                Equipamento = i.Equipamento?.NomeEquip,
                                NumSerie = i.EquipamentoContrato?.NumSerie,
                                BemTradeIn = i.BemTradeIn,
                                PedidoCompra = i.PedidoCompra,  
                                ValorInstalacao = string.Format("{0:C}", i.Contrato?.ContratosEquipamento?
                                                    .Where(ce => ce?.CodEquip == i.CodEquip)
                                                    .Sum(ce => ce.VlrInstalacao))
                            });

            var wsOs = Workbook.Worksheets.Add("instalacoesPleito");
            wsOs.Cell(2, 1).Value = sheet;
            WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}