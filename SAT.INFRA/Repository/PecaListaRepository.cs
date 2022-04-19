using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PecaListaRepository : IPecaListaRepository
    {
        private readonly AppDbContext _context;

        public PecaListaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<PecaLista> ObterPorParametros(PecaListaParameters parameters)
        {
            IQueryable<PecaLista> pecasLista = _context.PecaLista.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                pecasLista = pecasLista.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<PecaLista>.ToPagedList(pecasLista, parameters.PageNumber, parameters.PageSize);
        }
    }
}
