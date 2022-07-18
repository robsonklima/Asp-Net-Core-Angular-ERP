using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketModuloService
    {
        ListViewModel ObterPorParametros(TicketModuloParameters parameters);
        TicketModulo ObterPorCodigo(int codigo);
        TicketModulo Atualizar(TicketModulo ticketModulo);
    }
}
