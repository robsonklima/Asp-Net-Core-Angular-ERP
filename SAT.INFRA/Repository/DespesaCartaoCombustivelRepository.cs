using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repositories
{
    public class DespesaCartaoCombustivelRepository : IDespesaCartaoCombustivelRepository
    {
        private readonly AppDbContext _context;

        public DespesaCartaoCombustivelRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<DespesaCartaoCombustivel> ObterPorParametros(DespesaCartaoCombustivelParameters parameters)
        {
            var cartoes = _context.DespesaCartaoCombustivel.AsQueryable();

            if (parameters.Filter != null)
            {
                cartoes = cartoes.Where(
                    c =>
                    c.Numero.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodDespesaCartaoCombustivel != null)
            {
                cartoes = cartoes.Where(a => a.CodDespesaCartaoCombustivel == parameters.CodDespesaCartaoCombustivel);
            }

            if (parameters.IndAtivo != null)
            {
                cartoes = cartoes.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                cartoes = cartoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<DespesaCartaoCombustivel>.ToPagedList(cartoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
