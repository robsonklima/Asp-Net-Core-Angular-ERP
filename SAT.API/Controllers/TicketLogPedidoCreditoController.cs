using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketLogPedidoCreditoController : ControllerBase
    {
        private readonly ITicketLogPedidoCreditoService _ticketLogPedidoCreditoService;

        public TicketLogPedidoCreditoController(ITicketLogPedidoCreditoService ticketLogPedidoCreditoService)
        {
            _ticketLogPedidoCreditoService = ticketLogPedidoCreditoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketLogPedidoCreditoParameters parameters)
        {
            return _ticketLogPedidoCreditoService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public void Post([FromBody] TicketLogPedidoCredito pedidoCredito)
        {
            _ticketLogPedidoCreditoService.Criar(pedidoCredito);
        }

        [HttpDelete("{codTicketLogPedidoCredito}")]
        public void Delete(int codTicketLogPedidoCredito)
        {
            _ticketLogPedidoCreditoService.Deletar(codTicketLogPedidoCredito);
        }
    }
}