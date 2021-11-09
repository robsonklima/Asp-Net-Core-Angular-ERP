using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        public IQueryable<Filial> AplicarFiltros(IQueryable<Filial> query, FilialParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case FilialFilterEnum.FILTER_DASHBOARD_DISPONIBILIDADE_TECNICOS:
                    query = AplicarFiltroDashboardDisponibilidadeTecnicos(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }

            return query;
        }
    }
}
