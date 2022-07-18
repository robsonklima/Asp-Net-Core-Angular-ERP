using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketPrioridadeService : ITicketPrioridadeService
    {
        private readonly ITicketPrioridadeRepository _ticketPrioridadeRepo;

        public TicketPrioridadeService(
            ITicketPrioridadeRepository ticketPrioridadeRepo
        )
        {
            _ticketPrioridadeRepo = ticketPrioridadeRepo;
        }
        
        public TicketPrioridade ObterPorCodigo(int codigo)
        {
            return _ticketPrioridadeRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketPrioridadeParameters parameters)
        {
            var ticketPrioridade = _ticketPrioridadeRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                    Items = ticketPrioridade,
                TotalCount = ticketPrioridade.TotalCount,
                CurrentPage = ticketPrioridade.CurrentPage,
                PageSize = ticketPrioridade.PageSize,
                TotalPages = ticketPrioridade.TotalPages,
                HasNext = ticketPrioridade.HasNext,
                HasPrevious = ticketPrioridade.HasPrevious
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


        public TicketPrioridade Atualizar(TicketPrioridade ticketPrioridade)
        {

            _ticketPrioridadeRepo.Atualizar(ticketPrioridade);
            return ticketPrioridade;
        }

        // public Ticket Criar(EquipamentoContrato equipamentoContrato)
        // {
        //     throw new System.NotImplementedException();
        // }


    }
}
