using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITicketLogPedidoCreditoService
    {
        TicketLogPedidoCredito Criar(TicketLogPedidoCredito pedidoCredito);
        ListViewModel ObterPorParametros(TicketLogPedidoCreditoParameters parameters);
        void Deletar(int codigo);
    }
}
