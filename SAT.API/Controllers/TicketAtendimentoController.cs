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
    public class TicketAtendimentoController : ControllerBase
    {
        private readonly ITicketAtendimentoService _ticketAtendimentoService;

        public TicketAtendimentoController(
            ITicketAtendimentoService ticketAtendimentoService
        )
        {
            _ticketAtendimentoService = ticketAtendimentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TicketAtendimentoParameters parameters)
        {
            return _ticketAtendimentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTicketAtend}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TicketAtendimento Get(int codTicketAtend)
        {
            return _ticketAtendimentoService.ObterPorCodigo(codTicketAtend);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public TicketAtendimento Post([FromBody] TicketAtendimento ticketAtendimento)
        {
            return _ticketAtendimentoService.Criar(ticketAtendimento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public TicketAtendimento Put([FromBody] TicketAtendimento ticketAtendimento)
        {
            return _ticketAtendimentoService.Atualizar(ticketAtendimento);
        }

        [HttpDelete("{codTicketAtend}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public TicketAtendimento Delete(int codTicketAtend)
        {
            return _ticketAtendimentoService.Deletar(codTicketAtend);
        }
    }
}
