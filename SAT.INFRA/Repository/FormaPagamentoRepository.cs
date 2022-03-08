using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly AppDbContext _context;

        public FormaPagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<FormaPagamento> ObterPorParametros(FormaPagamentoParameters parameters)
        {
           IQueryable<FormaPagamento> formasPagamento = _context.FormaPagamento.AsQueryable();

            if (parameters.IndAtivo != null)
            {
                formasPagamento = formasPagamento.Where(p => p.IndAtivo == parameters.IndAtivo.Value);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                formasPagamento = formasPagamento.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<FormaPagamento>.ToPagedList(formasPagamento, parameters.PageNumber, parameters.PageSize);
        }
    }
}
