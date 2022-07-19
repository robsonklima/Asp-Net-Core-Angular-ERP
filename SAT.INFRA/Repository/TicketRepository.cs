using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
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

            if (parameters.CodUsuario != null)
            {
                query = query.Where(t => t.CodUsuario == parameters.CodUsuario);
            }
            if (parameters.CodModulo.HasValue)
            {
                query = query.Where(t => t.CodModulo == parameters.CodModulo);
            }
            if (parameters.CodStatus.HasValue)
            {
                query = query.Where(t => t.CodStatus == parameters.CodStatus);
            }
            if (parameters.CodPrioridade.HasValue)
            {
                query = query.Where(t => t.CodPrioridade == parameters.CodPrioridade);
            }
            if (parameters.CodClassificacao.HasValue)
            {
                query = query.Where(t => t.CodClassificacao == parameters.CodClassificacao);
            }

            return PagedList<Ticket>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public Ticket ObterPorCodigo(int codTicket)
        {
            return _context.Ticket
            .Include(t => t.TicketModulo)
                                .Include(t => t.TicketPrioridade)
                                .Include(t => t.TicketClassificacao)
                                .Include(t => t.TicketStatus)
                                .Include(t => t.Usuario)
                                    .ThenInclude(t => t.Filial)
            .FirstOrDefault(t => t.CodTicket == codTicket);
        }

        public void Atualizar(Ticket ticket)
        {

            _context.ChangeTracker.Clear();
            Ticket tick = _context.Ticket.SingleOrDefault(t => t.CodTicket == ticket.CodTicket);

            if (tick != null)
            {
                try
                {
                    _context.Entry(tick).CurrentValues.SetValues(ticket);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }
    }
}