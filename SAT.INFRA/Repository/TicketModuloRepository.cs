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
    public class TicketModuloRepository : ITicketModuloRepository
    {
        private readonly AppDbContext _context;

        public TicketModuloRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketModulo> ObterPorParametros(TicketModuloParameters parameters)
        {
            var query = _context
                            .TicketModulo
                            .AsQueryable();

            return PagedList<TicketModulo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketModulo ObterPorCodigo(int codModulo)
        {
            return _context.TicketModulo
            .FirstOrDefault(t => t.CodModulo == codModulo);
        }

        public void Atualizar(TicketModulo ticketModulo)
        {

            _context.ChangeTracker.Clear();
            TicketModulo tick = _context.TicketModulo.SingleOrDefault(t => t.CodModulo == ticketModulo.CodModulo);

            if (tick != null)
            {
                try
                {
                    _context.Entry(tick).CurrentValues.SetValues(ticketModulo);
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