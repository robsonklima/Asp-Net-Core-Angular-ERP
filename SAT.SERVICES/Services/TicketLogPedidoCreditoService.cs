using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

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
            if (!ExisteCreditoDoPeriodo((int)pedidoCredito.CodDespesaPeriodoTecnico)) 
                _ticketLogPedidoCreditoRepo.Criar(pedidoCredito);
            return pedidoCredito;
        }

        public ListViewModel ObterPorParametros(TicketLogPedidoCreditoParameters parameters)
        {
            var tickets = _ticketLogPedidoCreditoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = tickets,
                TotalCount = tickets.TotalCount,
                CurrentPage = tickets.CurrentPage,
                PageSize = tickets.PageSize,
                TotalPages = tickets.TotalPages,
                HasNext = tickets.HasNext,
                HasPrevious = tickets.HasPrevious
            };
        }

        private bool ExisteCreditoDoPeriodo(int codDespesaPeriodoTecnico) {
            var creditos = _ticketLogPedidoCreditoRepo.ObterPorParametros(new TicketLogPedidoCreditoParameters() {
                CodDespesaPeriodoTecnico = codDespesaPeriodoTecnico
            });

            if (creditos.TotalCount > 0) 
                return true;

            return false;
        }

        public void Deletar(int codigo)
        {
            _ticketLogPedidoCreditoRepo.Deletar(codigo);
        }
    }
}