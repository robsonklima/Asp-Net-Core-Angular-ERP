using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketPrioridadeService
    {
        ListViewModel ObterPorParametros(TicketPrioridadeParameters parameters);
        TicketPrioridade ObterPorCodigo(int codigo);
        TicketPrioridade Atualizar(TicketPrioridade ticketPrioridade);
    }
}
