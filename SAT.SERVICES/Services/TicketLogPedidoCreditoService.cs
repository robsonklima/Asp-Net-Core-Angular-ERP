using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketLogPedidoCreditoService : ITicketLogPedidoCreditoService
    {
        private readonly ITicketLogPedidoCreditoRepository _ticketLogPedidoCreditoRepo;

        public TicketLogPedidoCreditoService(
            ITicketLogPedidoCreditoRepository ticketLogPedidoCreditoRepo)
        {
            _ticketLogPedidoCreditoRepo = ticketLogPedidoCreditoRepo;
        }

        public TicketLogPedidoCredito Criar(TicketLogPedidoCredito pedidoCredito)
        {
            _ticketLogPedidoCreditoRepo.Criar(pedidoCredito);
            return pedidoCredito;
        }
    }
}