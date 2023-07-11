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
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketLogTransacaoController : ControllerBase
    {
        private readonly ITicketLogTransacaoService _ticketLogTransacaoService;

        public TicketLogTransacaoController(ITicketLogTransacaoService ticketLogTransacaoService)
        {
            _ticketLogTransacaoService = ticketLogTransacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketLogTransacaoParameters parameters)
        {
            return _ticketLogTransacaoService.ObterPorParametros(parameters);
        }
    }
}