using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository {
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<Ticket> ObterPorParametros(TicketParameters parameters)
        {
            var query = _context.Ticket.AsQueryable();

            if (parameters.CodUsuario != null) {
                query = query.Where(t => t.CodUsuario == parameters.CodUsuario);
            }

            return PagedList<Ticket>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}