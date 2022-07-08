using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketService
    {
        ListViewModel ObterPorParametros(TicketParameters parameters);
        Ticket ObterPorCodigo(int codigo);
        Ticket Atualizar(Ticket ticket);
    }
}
