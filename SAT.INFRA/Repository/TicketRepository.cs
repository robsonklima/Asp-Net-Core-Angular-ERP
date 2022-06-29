using Microsoft.EntityFrameworkCore;
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
            var query = _context
                            .Ticket
                                .Include(t => t.TicketModulo)
                                .Include(t => t.TicketPrioridade)
                                .Include(t => t.TicketClassificacao)
                                .Include(t => t.TicketStatus)
                                .Include(t => t.Usuario)
                                    .ThenInclude(t => t.Filial)
                                .AsQueryable();

            if (parameters.CodUsuario != null) {
                query = query.Where(t => t.CodUsuario == parameters.CodUsuario);
            }

            return PagedList<Ticket>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public Ticket ObterPorCodigo(int codTicket)
        {
            return _context.Ticket.FirstOrDefault(t => t.CodTicket == codTicket);
        }
    }
}