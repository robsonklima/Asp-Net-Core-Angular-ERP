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
    public class TicketPrioridadeRepository : ITicketPrioridadeRepository
    {
        private readonly AppDbContext _context;

        public TicketPrioridadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketPrioridade> ObterPorParametros(TicketPrioridadeParameters parameters)
        {
            var query = _context
                            .TicketPrioridade
                                .AsQueryable();

            // if (parameters.CodPrioridade != null)
            // {
            //     query = query.Where(t => t.CodPrioridade == parameters.CodPrioridade);
            // }

            return PagedList<TicketPrioridade>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketPrioridade ObterPorCodigo(int CodPrioridade)
        {
            return _context.TicketPrioridade
            .FirstOrDefault(t => t.CodPrioridade == CodPrioridade);
        }

        public void Atualizar(TicketPrioridade ticketPrioridade)
        {

            _context.ChangeTracker.Clear();
            TicketPrioridade tick = _context.TicketPrioridade.SingleOrDefault(t => t.CodPrioridade == ticketPrioridade.CodPrioridade);

            if (tick != null)
            {
                try
                {
                    _context.Entry(tick).CurrentValues.SetValues(ticketPrioridade);
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