using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
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
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }

            return query;
        }
    }
}
