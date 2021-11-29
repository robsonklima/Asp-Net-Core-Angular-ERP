using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.INFRA.Interfaces;

namespace SAT.INFRA.Repository
{
    public class TicketLogPedidoCreditoRepository : ITicketLogPedidoCreditoRepository
    {
        private readonly AppDbContext _context;

        public TicketLogPedidoCreditoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(TicketLogPedidoCredito pedidoCredito)
        {
            _context.Add(pedidoCredito);
            _context.SaveChanges();
        }
    }
}