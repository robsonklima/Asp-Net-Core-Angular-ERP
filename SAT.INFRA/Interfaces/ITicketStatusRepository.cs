using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketStatusRepository {
        PagedList<TicketStatus> ObterPorParametros(TicketStatusParameters parameters);
        TicketStatus ObterPorCodigo(int codStatus);
        void Atualizar(TicketStatus status);

    }
}