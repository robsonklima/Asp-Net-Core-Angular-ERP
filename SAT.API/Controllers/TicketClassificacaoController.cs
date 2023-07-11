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
    public class TicketClassificacaoController : ControllerBase
    {
        private readonly ITicketClassificacaoService _ticketClassificacaoService;

        public TicketClassificacaoController(
            ITicketClassificacaoService ticketClassificacaoService
        )
        {
            _ticketClassificacaoService = ticketClassificacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketClassificacaoParameters parameters)
        {
            return _ticketClassificacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codClassificacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TicketClassificacao Get(int codClassificacao)
        {
            return _ticketClassificacaoService.ObterPorCodigo(codClassificacao);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TicketClassificacao ticketClassificacao)
        {
            _ticketClassificacaoService.Atualizar(ticketClassificacao);
        }
    }
}
