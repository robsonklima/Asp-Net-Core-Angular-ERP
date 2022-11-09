using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
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
            var query = _context.TicketAtendimento
                .Include(t => t.TicketStatus)
                .Include(t => t.UsuarioCad)
                .Include(t => t.UsuarioManut)
                .AsQueryable();

            if (parameters.CodUsuarioCad != null)
            {
                query = query.Where(t => t.CodUsuarioCad == parameters.CodUsuarioCad);
            }

            if (parameters.CodTicket != null)
            {
                query = query.Where(t => t.CodTicket == parameters.CodTicket);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketAtendimento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        
        public TicketAtendimento ObterPorCodigo(int codTicketAtendimento)
        {
            return _context.TicketAtendimento
                .Include(t => t.TicketStatus)
                .Include(t => t.UsuarioCad)
                .Include(t => t.UsuarioManut)
                .FirstOrDefault(t => t.CodTicketAtend == codTicketAtendimento);
        }

        public TicketAtendimento Atualizar(TicketAtendimento atendimento)
        {
            _context.ChangeTracker.Clear();
            TicketAtendimento t = _context.TicketAtendimento.FirstOrDefault(t => t.CodTicketAtend == atendimento.CodTicketAtend);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(atendimento);
                _context.SaveChanges();
            }

            return t;
        }

        public TicketAtendimento Criar(TicketAtendimento atendimento)
        {
            _context.Add(atendimento);
            _context.SaveChanges();
            return atendimento;
        }

        public TicketAtendimento Deletar(int cod)
        {
            TicketAtendimento atendimento = _context.TicketAtendimento.FirstOrDefault(t => t.CodTicketAtend == cod);

            if (atendimento != null)
            {
                _context.TicketAtendimento.Remove(atendimento);
                _context.SaveChanges();
            }

            return atendimento;
        }
    }
}