using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepo;

        public TicketService(
            ITicketRepository ticketRepo
        )
        {
            _ticketRepo = ticketRepo;
        }
        
        public Ticket ObterPorCodigo(int codigo)
        {
            return _ticketRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketParameters parameters)
        {
            var tickets = _ticketRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tickets,
                TotalCount = tickets.TotalCount,
                CurrentPage = tickets.CurrentPage,
                PageSize = tickets.PageSize,
                TotalPages = tickets.TotalPages,
                HasNext = tickets.HasNext,
                HasPrevious = tickets.HasPrevious
            };

            return lista;
        }

        public List<TicketBacklogView> ObterBacklog(TicketParameters parameters)
        {
            return _ticketRepo.ObterBacklog(parameters);
        }

        public Ticket Atualizar(Ticket ticket)
        {

            _ticketRepo.Atualizar(ticket);
            return ticket;
        }

        public Ticket Criar(Ticket ticket)
        {
            return _ticketRepo.Criar(ticket);
        }

        public Ticket Deletar(int codigo)
        {
            return _ticketRepo.Deletar(codigo);
        }
    }
}
