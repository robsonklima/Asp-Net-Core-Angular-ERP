using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class TicketLogTransacaoRepository : ITicketLogTransacaoRepository
    {
        private readonly AppDbContext _context;
        public TicketLogTransacaoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public PagedList<TicketLogTransacao> ObterPorParametros(TicketLogTransacaoParameters parameters)
        {
            var query = _context.TicketLogTransacao.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketLogTransacao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
