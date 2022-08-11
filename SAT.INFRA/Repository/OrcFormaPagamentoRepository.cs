using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository {
    public class OrcFormaPagamentoRepository : IOrcFormaPagamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcFormaPagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<OrcFormaPagamento> ObterPorParametros(OrcFormaPagamentoParameters parameters)
        {                        
            var query = _context.OrcFormaPagamento.AsQueryable();
            
            if (parameters.Filter != null)
            {
                query = query.Where(f => f.Nome.Contains(parameters.Filter));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null) 
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrcFormaPagamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}