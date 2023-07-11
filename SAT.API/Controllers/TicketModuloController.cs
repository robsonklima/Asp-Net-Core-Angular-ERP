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
    public class TicketModuloController : ControllerBase
    {
        private readonly ITicketModuloService _ticketModuloService;

        public TicketModuloController(
            ITicketModuloService ticketModuloService
        )
        {
            _ticketModuloService = ticketModuloService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketModuloParameters parameters)
        {
            return _ticketModuloService.ObterPorParametros(parameters);
        }

        [HttpGet("{codModulo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TicketModulo Get(int codModulo)
        {
            return _ticketModuloService.ObterPorCodigo(codModulo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TicketModulo ticketModulo)
        {
            _ticketModuloService.Atualizar(ticketModulo);
        }
    }
}
