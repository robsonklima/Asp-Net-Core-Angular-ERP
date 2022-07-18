using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketClassificacaoService : ITicketClassificacaoService
    {
        private readonly ITicketClassificacaoRepository _ticketClassificacaoRepo;

        public TicketClassificacaoService(
            ITicketClassificacaoRepository ticketClassificacaoRepo
        )
        {
            _ticketClassificacaoRepo = ticketClassificacaoRepo;
        }
        
        public TicketClassificacao ObterPorCodigo(int codigo)
        {
            return _ticketClassificacaoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketClassificacaoParameters parameters)
        {
            var ticketClassificacao = _ticketClassificacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                    Items = ticketClassificacao,
                TotalCount = ticketClassificacao.TotalCount,
                CurrentPage = ticketClassificacao.CurrentPage,
                PageSize = ticketClassificacao.PageSize,
                TotalPages = ticketClassificacao.TotalPages,
                HasNext = ticketClassificacao.HasNext,
                HasPrevious = ticketClassificacao.HasPrevious
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


        public TicketClassificacao Atualizar(TicketClassificacao ticketClassificacao)
        {

            _ticketClassificacaoRepo.Atualizar(ticketClassificacao);
            return ticketClassificacao;
        }

        // public Ticket Criar(EquipamentoContrato equipamentoContrato)
        // {
        //     throw new System.NotImplementedException();
        // }


    }
}
