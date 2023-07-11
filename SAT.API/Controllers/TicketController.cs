using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketParameters parameters)
        {
            return _ticketService.ObterPorParametros(parameters);
        }

        [HttpGet("Backlog")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<TicketBacklogView> GetBacklog([FromQuery] TicketParameters parameters)
        {
            return _ticketService.ObterBacklog(parameters);
        }

        [HttpGet("{codTicket}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Ticket Get(int codTicket)
        {
            return _ticketService.ObterPorCodigo(codTicket);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Ticket Post([FromBody] Ticket ticket)
        {
            return _ticketService.Criar(ticket);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public Ticket Put([FromBody] Ticket ticket)
        {
            return _ticketService.Atualizar(ticket);
        }

        [HttpDelete("{codTicket}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public Ticket Delete(int codTicket)
        {
            return _ticketService.Deletar(codTicket);
        }
    }
}
