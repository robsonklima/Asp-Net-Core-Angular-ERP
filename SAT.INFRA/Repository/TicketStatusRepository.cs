using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TicketStatusRepository : ITicketStatusRepository
    {
        private readonly AppDbContext _context;

        public TicketStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketStatus> ObterPorParametros(TicketStatusParameters parameters)
        {
            var query = _context.TicketStatus.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketStatus ObterPorCodigo(int codStatus)
        {
            return _context.TicketStatus.FirstOrDefault(t => t.CodStatus == codStatus);
        }

        public void Atualizar(TicketStatus ticketStatus)
        {
            _context.ChangeTracker.Clear();
            TicketStatus tick = _context.TicketStatus.SingleOrDefault(t => t.CodStatus == ticketStatus.CodStatus);

            if (tick != null)
            {
                _context.Entry(tick).CurrentValues.SetValues(ticketStatus);
                _context.SaveChanges();
            }
        }
    }
}