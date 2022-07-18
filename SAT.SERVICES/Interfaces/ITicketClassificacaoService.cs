using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketClassificacaoService
    {
        ListViewModel ObterPorParametros(TicketClassificacaoParameters parameters);
        TicketClassificacao ObterPorCodigo(int codigo);
        TicketClassificacao Atualizar(TicketClassificacao ticketClassificacao);
    }
}
