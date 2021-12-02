using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class PecaRepository : IPecaRepository
    {
        public IQueryable<Peca> AplicarFiltros(IQueryable<Peca> query, PecaParameters parameters)
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
