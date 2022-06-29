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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(
            ITicketService ticketService
        )
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketParameters parameters)
        {
            return _ticketService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTicket}")]
        public Ticket Get(int codTicket)
        {
            return _ticketService.ObterPorCodigo(codTicket);
        }

        // [HttpPost]
        // public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        // {
        //     return _equipamentoContratoService.Criar(equipamentoContrato);
        // }

        // [HttpPut]
        // public void Put([FromBody] EquipamentoContrato equipamentoContrato)
        // {
        //     _equipamentoContratoService.Atualizar(equipamentoContrato);
        // }

        // [HttpDelete("{codEquipContrato}")]
        // public void Delete(int codEquipContrato)
        // {
        //     _equipamentoContratoService.Deletar(codEquipContrato);
        // }
    }
}
