using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketPrioridadeRepository {
        PagedList<TicketPrioridade> ObterPorParametros(TicketPrioridadeParameters parameters);
        TicketPrioridade ObterPorCodigo(int CodPrioridade);
        void Atualizar(TicketPrioridade ticketPrioridade);

    }
}