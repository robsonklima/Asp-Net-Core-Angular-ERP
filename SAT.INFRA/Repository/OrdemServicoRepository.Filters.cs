using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltros(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case (OrdemServicoFilterEnum.FILTER_AGENDA):
                    query = AplicarFiltroAgendaTecnico(query, parameters);
                    break;
                case OrdemServicoFilterEnum.FILTER_INDICADOR:
                    query = AplicarFiltroIndicadores(query, parameters);
                    break;
                case (OrdemServicoFilterEnum.FILTER_CORRETIVAS_ANTIGAS):
                    query = AplicarFiltroChamadosMaisAntigos(query, parameters, TipoIntervencaoEnum.CORRETIVA);
                    break;
                case (OrdemServicoFilterEnum.FILTER_ORCAMENTOS_ANTIGOS):
                    query = AplicarFiltroChamadosMaisAntigos(query, parameters, TipoIntervencaoEnum.ORC_APROVADO);
                    break;
                case (OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES):
                    query = AplicarFiltroPecasFaltantesFiliais(query, parameters);
                    break;
                case OrdemServicoFilterEnum.FILTER_DISPONIBILIDADE_BB_CALCULA_OS:
                    query = AplicarFiltroDisponiblidadeBBCalculaOS(query);
                    break;
                case OrdemServicoFilterEnum.FILTER_GENERIC_TEXT:
                    query = AplicarFiltroGenericText(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }
            return query;
        }
    }
}