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
    public class TicketPrioridadeController : ControllerBase
    {
        private readonly ITicketPrioridadeService _ticketPrioridadeService;

        public TicketPrioridadeController(
            ITicketPrioridadeService ticketPrioridadeService
        )
        {
            _ticketPrioridadeService = ticketPrioridadeService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketPrioridadeParameters parameters)
        {
            return _ticketPrioridadeService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPrioridade}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TicketPrioridade Get(int codPrioridade)
        {
            return _ticketPrioridadeService.ObterPorCodigo(codPrioridade);
        }
        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TicketPrioridade ticketPrioridade)
        {
            _ticketPrioridadeService.Atualizar(ticketPrioridade);
        }
    }
}
