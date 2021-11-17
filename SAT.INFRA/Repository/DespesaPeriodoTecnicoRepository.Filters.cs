using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltros(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case MODELS.Enums.DespesaPeriodoTecnicoFilterEnum.FILTER_PERIODOS_APROVADOS:
                    query = AplicarFiltroPeriodosAprovados(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }
            return query;
        }
    }
}