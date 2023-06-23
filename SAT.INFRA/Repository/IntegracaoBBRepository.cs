using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using System.Linq;
using SAT.MODELS.Views;

namespace SAT.INFRA.Repository
{
    public class IntegracaoBBRepository : IIntegracaoBBRepository
    {
        private readonly AppDbContext _context;

        public IntegracaoBBRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<ViewIntegracaoBB> ObterPorParametros(IntegracaoBBParameters parameters)
        {
            var query = _context.ViewIntegracaoBB.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ViewIntegracaoBB>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}