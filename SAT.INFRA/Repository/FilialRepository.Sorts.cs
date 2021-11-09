using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        public IQueryable<Filial> AplicarOrdenacao(IQueryable<Filial> query, string sortActive, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortActive) && !string.IsNullOrEmpty(sortDirection))
            {
                switch (sortActive)
                {
                    default:
                        query = query.OrderBy(string.Format("{0} {1}", sortActive, sortDirection));
                        break;
                }
            }

            return query;
        }
    }
}
