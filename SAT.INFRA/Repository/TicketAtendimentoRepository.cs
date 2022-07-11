using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TicketAtendimentoRepository : ITicketAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public TicketAtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketAtendimento> ObterPorParametros(TicketAtendimentoParameters parameters)
        {
            var query = _context
                            .TicketAtendimento
                                .Include(t => t.TicketStatus)
                                .Include(t => t.Usuario)
                                .AsQueryable();

            if (parameters.UsuarioAtend != null)
            {
                query = query.Where(t => t.UsuarioAtend == parameters.UsuarioAtend);
            }

            if (parameters.CodTicket != null)
            {
                query = query.Where(t => t.CodTicket == parameters.CodTicket);
            }

            return PagedList<TicketAtendimento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketAtendimento ObterPorCodigo(int codTicketAtendimento)
        {
            return _context.TicketAtendimento
                                .Include(t => t.TicketStatus)
                                .Include(t => t.Usuario)
                                .FirstOrDefault(t => t.CodTicketAtend == codTicketAtendimento);
        }
    }
}