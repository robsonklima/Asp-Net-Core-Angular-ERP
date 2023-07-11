using System.Security.Claims;
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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketAnexoParameters parameters)
        {
            return _ticketAnexoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTicketAnexo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TicketAnexo Get(int codTicketAnexo)
        {
            return _ticketAnexoService.ObterPorCodigo(codTicketAnexo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public TicketAnexo Post([FromBody] TicketAnexo ticketAnexo)
        {
            return _ticketAnexoService.Criar(ticketAnexo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public TicketAnexo Put([FromBody] TicketAnexo ticketAnexo)
        {
            return _ticketAnexoService.Atualizar(ticketAnexo);
        }

        [HttpDelete("{codTicketAnexo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public TicketAnexo Delete(int codTicketAnexo)
        {
            return _ticketAnexoService.Deletar(codTicketAnexo);
        }
    }
}
