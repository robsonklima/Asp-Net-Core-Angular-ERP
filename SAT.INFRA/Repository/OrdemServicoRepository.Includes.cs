using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarIncludes(IQueryable<OrdemServico> query, OrdemServicoIncludeEnum include)
        {
            switch (include)
            {
                case (OrdemServicoIncludeEnum.OS_RAT):
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Tecnico)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.RelatoriosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_AGENDA):
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Tecnico)
                        .Include(os => os.LocalAtendimento)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.RegiaoAutorizada.Autorizada)
                        .Include(os => os.RegiaoAutorizada.Regiao);
                    break;

                default:
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.LocalAtendimento)
                        .Include(os => os.LocalAtendimento.Cidade)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                        .Include(os => os.Equipamento)
                        .Include(os => os.EquipamentoContrato)
                        .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                        .Include(os => os.RegiaoAutorizada)
                        .Include(os => os.RegiaoAutorizada.Filial)
                        .Include(os => os.RegiaoAutorizada.Autorizada)
                        .Include(os => os.RegiaoAutorizada.Regiao)
                        .Include(os => os.Cliente)
                        .Include(os => os.Cliente.Cidade)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.Agendamentos)
                        .Include(os => os.Tecnico)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(rat => rat.RelatorioAtendimentoDetalhes)
                                .ThenInclude(ratd => ratd.RelatorioAtendimentoDetalhePecas)
                                    .ThenInclude(ratdp => ratdp.Peca)
                        .Include(os => os.EquipamentoContrato.Contrato)
                        .Include(os => os.PrazosAtendimento);
                    break;
            }

            return query;
        }
    }
}
