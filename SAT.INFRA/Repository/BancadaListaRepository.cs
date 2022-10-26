using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class BancadaListaRepository : IBancadaListaRepository
    {
        private readonly AppDbContext _context;

        public BancadaListaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<BancadaLista> ObterPorParametros(BancadaListaParameters parameters)
        {
            IQueryable<BancadaLista> listaBancadas = _context.BancadaLista.AsQueryable();

            if (parameters.IndAtivo != null)
            {
                listaBancadas = listaBancadas.Where(p => p.IndAtivo == parameters.IndAtivo.Value);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                listaBancadas = listaBancadas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<BancadaLista>.ToPagedList(listaBancadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
