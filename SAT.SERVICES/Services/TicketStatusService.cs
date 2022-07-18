using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketStatusService : ITicketStatusService
    {
        private readonly ITicketStatusRepository _ticketRepo;

        public TicketStatusService(
            ITicketStatusRepository ticketRepo
        )
        {
            _ticketRepo = ticketRepo;
        }
        
        public TicketStatus ObterPorCodigo(int codigo)
        {
            return _ticketRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketStatusParameters parameters)
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

     

        // public Ticket Criar(Ticket ticket)
        // {
        //     // _ticketRepo.Criar(ticket);
        //     return null;
        //     // return ticket;
        // }

        // public void Deletar(int codigo)
        // {
        //     return null;
        //     // _ticketRepo.Deletar(codigo);
        // }


        public TicketStatus Atualizar(TicketStatus ticketStatus)
        {

            _ticketRepo.Atualizar(ticketStatus);
            return ticketStatus;
        }

        // public Ticket Criar(EquipamentoContrato equipamentoContrato)
        // {
        //     throw new System.NotImplementedException();
        // }


    }
}
