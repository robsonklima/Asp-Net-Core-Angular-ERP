using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class OrcamentoStatusRepository : IOrcamentoStatusRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<OrcamentoStatus> ObterPorParametros(OrcamentoStatusParameters parameters)
        {
            var query =
                _context.OrcamentoStatus.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<OrcamentoStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}