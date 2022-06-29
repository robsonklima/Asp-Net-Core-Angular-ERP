using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITicketLogPedidoCreditoRepository
    {
        void Criar(TicketLogPedidoCredito pedidoCredito);
        PagedList<TicketLogPedidoCredito> ObterPorParametros(TicketLogPedidoCreditoParameters parameters);
    }
}
