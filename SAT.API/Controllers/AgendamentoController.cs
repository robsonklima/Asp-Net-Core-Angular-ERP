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
    public class AgendamentoController : ControllerBase
    {
        private IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AgendamentoParameters parameters)
        {
            return _agendamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAgendamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Agendamento Get(int codAgendamento)
        {
            return _agendamentoService.ObterPorCodigo(codAgendamento);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Agendamento Post([FromBody] Agendamento agendamento)
        {
            return _agendamentoService.Criar(agendamento);
        }

        [HttpPut("{codAgendamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put(int codAgendamento, [FromBody] Agendamento agendamento)
        {
            _agendamentoService.Atualizar(agendamento);
        }

        [HttpDelete("{codAgendamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAgendamento)
        {
            _agendamentoService.Deletar(codAgendamento);
        }
    }
}
