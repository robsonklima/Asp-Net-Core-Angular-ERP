using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        public IQueryable<RegiaoAutorizada> AplicarOrdenacao(IQueryable<RegiaoAutorizada> query, string sortActive, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortActive) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(string.Format("{0} {1}", sortActive, sortDirection));
            }

            return query;
        }
    }
}
