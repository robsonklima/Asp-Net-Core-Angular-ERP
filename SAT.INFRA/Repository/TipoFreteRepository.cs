using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoFreteRepository : ITipoFreteRepository
    {
        private readonly AppDbContext _context;

        public TipoFreteRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TipoFrete> ObterPorParametros(TipoFreteParameters parameters)
        {
            IQueryable<TipoFrete> tiposFrete = _context.TipoFrete.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tiposFrete = tiposFrete.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<TipoFrete>.ToPagedList(tiposFrete, parameters.PageNumber, parameters.PageSize);
        }
    }
}
