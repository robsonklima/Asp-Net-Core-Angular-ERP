using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class MoedaRepository : IMoedaRepository
    {
        private readonly AppDbContext _context;

        public MoedaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<Moeda> ObterPorParametros(MoedaParameters parameters)
        {
            IQueryable<Moeda> moedas = _context.Moeda.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                moedas = moedas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Moeda>.ToPagedList(moedas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
