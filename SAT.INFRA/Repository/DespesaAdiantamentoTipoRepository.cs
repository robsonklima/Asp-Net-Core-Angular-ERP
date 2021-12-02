using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DespesaAdiantamentoTipoRepository : IDespesaAdiantamentoTipoRepository
    {
        private readonly AppDbContext _context;

        public DespesaAdiantamentoTipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<DespesaAdiantamentoTipo> ObterPorParametros(DespesaAdiantamentoTipoParameters parameters)
        {
            var tipos = _context.DespesaAdiantamentoTipo.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                tipos = tipos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaAdiantamentoTipo>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}