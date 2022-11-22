using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
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
            var query = _context.Ticket
                .Include(t => t.TicketModulo)
                .Include(t => t.TicketPrioridade)
                .Include(t => t.TicketClassificacao)
                .Include(t => t.Atendimentos)
                .Include(t => t.TicketStatus)
                .Include(t => t.UsuarioAtendente)
                .Include(t => t.UsuarioCad.Filial)
                .Include(t => t.Anexos)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                query = query.Where(t => 
                                        t.Titulo.Contains(parameters.Filter) || 
                                        t.Descricao.Contains(parameters.Filter) ||
                                        t.UsuarioCad.NomeUsuario.Contains(parameters.Filter)
                                    );
            }

            if (parameters.CodUsuarioCad != null)
            {
                query = query.Where(t => t.CodUsuarioCad == parameters.CodUsuarioCad);
            }

            if (parameters.CodModulo.HasValue)
            {
                query = query.Where(t => t.CodModulo == parameters.CodModulo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodStatus))
            {
                int[] cods = parameters.CodStatus.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(t => cods.Contains(t.CodStatus));
            }

            if (parameters.CodPrioridade.HasValue)
            {
                query = query.Where(t => t.CodPrioridade == parameters.CodPrioridade);
            }

            if (parameters.CodClassificacao.HasValue)
            {
                query = query.Where(t => t.CodClassificacao == parameters.CodClassificacao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Ticket>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public Ticket ObterPorCodigo(int codTicket)
        {
            return _context.Ticket
                .Include(t => t.TicketModulo)
                .Include(t => t.TicketPrioridade)
                .Include(t => t.TicketClassificacao)
                .Include(t => t.TicketStatus)
                .Include(t => t.UsuarioCad)
                .Include(t => t.UsuarioCad.Filial)
                .Include(t => t.UsuarioManut)
                .Include(t => t.UsuarioManut.Filial)
                .Include(t => t.Atendimentos.OrderByDescending(a => a.DataHoraCad))
                    .ThenInclude(a => a.UsuarioCad)
                .Include(t => t.Atendimentos.OrderByDescending(a => a.DataHoraCad))
                    .ThenInclude(a => a.UsuarioManut)
                .Include(t => t.Atendimentos.OrderByDescending(a => a.DataHoraCad))
                    .ThenInclude(a => a.TicketStatus)
                .Include(t => t.Anexos)
                .Include(t => t.UsuarioAtendente)
                .FirstOrDefault(t => t.CodTicket == codTicket);
        }

        public Ticket Atualizar(Ticket ticket)
        {
            _context.ChangeTracker.Clear();
            Ticket t = _context.Ticket.FirstOrDefault(tc => tc.CodTicket == ticket.CodTicket);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(ticket);
                _context.SaveChanges();
            }

            return t;
        }

        public Ticket Criar(Ticket ticket)
        {
            _context.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public Ticket Deletar(int codigo)
        {
            Ticket t = _context.Ticket
                .Include(t => t.Atendimentos)
                .Include(t => t.Anexos)
                .FirstOrDefault(t => t.CodTicket == codigo);

            if (t != null)
            {
                _context.Ticket.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }
    }
}