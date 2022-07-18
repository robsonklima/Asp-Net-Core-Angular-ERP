using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketClassificacaoRepository {
        PagedList<TicketClassificacao> ObterPorParametros(TicketClassificacaoParameters parameters);
        TicketClassificacao ObterPorCodigo(int CodClassificacao);
        void Atualizar(TicketClassificacao ticketClassificacao);

    }
}