using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketAtendimentoService
    {
        ListViewModel ObterPorParametros(TicketAtendimentoParameters parameters);
        TicketAtendimento ObterPorCodigo(int codTicketAtendimento);
    }
}
