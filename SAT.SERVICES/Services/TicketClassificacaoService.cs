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

        public TicketClassificacao Atualizar(TicketClassificacao ticketClassificacao)
        {
            _ticketClassificacaoRepo.Atualizar(ticketClassificacao);
            return ticketClassificacao;
        }
    }
}
