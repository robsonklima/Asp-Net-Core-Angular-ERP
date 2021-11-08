using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> AplicarFiltros(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case TecnicoFilterEnum.FILTER_TECNICO_OS:
                    query = AplicarFiltroDashboardTecnicoDisponibilidade(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }

            return query;
        }
    }
}
