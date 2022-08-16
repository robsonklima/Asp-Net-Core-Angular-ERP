using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository {
    public class OrcDadosBancariosRepository : IOrcDadosBancariosRepository
    {
        private readonly AppDbContext _context;

        public OrcDadosBancariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<OrcDadosBancarios> ObterPorParametros(OrcDadosBancariosParameters parameters)
        {
            var query = _context.OrcDadosBancarios.AsQueryable();
            
            if (parameters.Filter != null)
            {
                query = query.Where(f => f.Banco.Contains(parameters.Filter));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null) 
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrcDadosBancarios>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}