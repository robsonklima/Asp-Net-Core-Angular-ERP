using System.Linq.Dynamic.Core;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TicketPrioridadeRepository : ITicketPrioridadeRepository
    {
        private readonly AppDbContext _context;

        public TicketPrioridadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketPrioridade> ObterPorParametros(TicketPrioridadeParameters parameters)
        {
            var query = _context.TicketPrioridade.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketPrioridade>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketPrioridade ObterPorCodigo(int CodPrioridade)
        {
            return _context.TicketPrioridade.FirstOrDefault(t => t.CodPrioridade == CodPrioridade);
        }

        public void Atualizar(TicketPrioridade ticketPrioridade)
        {
            _context.ChangeTracker.Clear();
            TicketPrioridade tick = _context.TicketPrioridade.SingleOrDefault(t => t.CodPrioridade == ticketPrioridade.CodPrioridade);

            if (tick != null)
            {
                _context.Entry(tick).CurrentValues.SetValues(ticketPrioridade);
                _context.SaveChanges();
            }
        }
    }
}