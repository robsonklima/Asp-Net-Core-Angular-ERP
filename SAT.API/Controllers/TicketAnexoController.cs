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
    public class TicketAnexoController : ControllerBase
    {
        private readonly ITicketAnexoService _ticketAnexoService;

        public TicketAnexoController(
            ITicketAnexoService ticketAnexoService
        )
        {
            _ticketAnexoService = ticketAnexoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketAnexoParameters parameters)
        {
            return _ticketAnexoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTicketAnexo}")]
        public TicketAnexo Get(int codTicketAnexo)
        {
            return _ticketAnexoService.ObterPorCodigo(codTicketAnexo);
        }

        [HttpPost]
        public TicketAnexo Post([FromBody] TicketAnexo ticketAnexo)
        {
            return _ticketAnexoService.Criar(ticketAnexo);
        }

        [HttpPut]
        public TicketAnexo Put([FromBody] TicketAnexo ticketAnexo)
        {
            return _ticketAnexoService.Atualizar(ticketAnexo);
        }

        [HttpDelete("{codTicketAnexo}")]
        public TicketAnexo Delete(int codTicketAnexo)
        {
            return _ticketAnexoService.Deletar(codTicketAnexo);
        }
    }
}
