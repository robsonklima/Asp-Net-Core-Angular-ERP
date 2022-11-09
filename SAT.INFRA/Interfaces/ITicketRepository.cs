using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketRepository {
        PagedList<Ticket> ObterPorParametros(TicketParameters parameters);
        Ticket ObterPorCodigo(int codTicket);
        Ticket Atualizar(Ticket ticket);
        Ticket Criar(Ticket ticket);
        Ticket Deletar(int codigo);
    }
}