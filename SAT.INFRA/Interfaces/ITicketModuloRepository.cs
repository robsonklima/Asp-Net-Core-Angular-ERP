using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketModuloRepository {
        PagedList<TicketModulo> ObterPorParametros(TicketModuloParameters parameters);
        TicketModulo ObterPorCodigo(int codModulo);
        void Atualizar(TicketModulo modulo);

    }
}