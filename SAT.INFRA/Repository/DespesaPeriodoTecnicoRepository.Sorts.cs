using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarOrdenacao(IQueryable<DespesaPeriodoTecnico> query, string sortActive, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortActive) && !string.IsNullOrEmpty(sortDirection))
                query = query.OrderBy(string.Format("{0} {1}", sortActive, sortDirection));

            return query;
        }
    }
}