using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PosVendaRepository : IPosVendaRepository
    {
        private readonly AppDbContext _context;

        public PosVendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PosVenda ObterPorCodigo(int codigo)
        {
            return _context.PosVenda.FirstOrDefault(t => t.CodPosVenda == codigo);
        }

        public PagedList<PosVenda> ObterPorParametros(PosVendaParameters parameters)
        {
            var posVendas = _context.PosVenda
                .AsQueryable();

            if (parameters.Filter != null)
            {
                posVendas = posVendas.Where(
                    p =>
                    p.CodPosVenda.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                posVendas = posVendas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<PosVenda>.ToPagedList(posVendas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
