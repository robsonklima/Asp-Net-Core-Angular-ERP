using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketStatusService
    {
        ListViewModel ObterPorParametros(TicketStatusParameters parameters);
        TicketStatus ObterPorCodigo(int codigo);
        TicketStatus Atualizar(TicketStatus ticketStatus);
    }
}
