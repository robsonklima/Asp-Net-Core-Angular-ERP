using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ITicketAnexoService
    {
        ListViewModel ObterPorParametros(TicketAnexoParameters parameters);
        TicketAnexo ObterPorCodigo(int codigo);
        TicketAnexo Criar(TicketAnexo anexo);
        TicketAnexo Deletar(int codigo);
        TicketAnexo Atualizar(TicketAnexo anexo);
    }
}
