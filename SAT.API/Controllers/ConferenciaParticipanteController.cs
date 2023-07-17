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
    public class ConferenciaParticipanteController : ControllerBase
    {
        private readonly IConferenciaParticipanteService _conferenciaParticipanteService;

        public ConferenciaParticipanteController(IConferenciaParticipanteService conferenciaParticipanteService)
        {
            _conferenciaParticipanteService = conferenciaParticipanteService;
        }

        [HttpGet("{codConferenciaParticipante}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ConferenciaParticipante Get(int codConferenciaParticipante)
        {
            return _conferenciaParticipanteService.ObterPorCodigo(codConferenciaParticipante);
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ConferenciaParticipanteParameters parameters)
        {
            return _conferenciaParticipanteService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public ConferenciaParticipante Post([FromBody] ConferenciaParticipante participante)
        {
            return _conferenciaParticipanteService.Criar(participante);
        }

        [HttpDelete("{codConferenciaParticipante}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codConferenciaParticipante)
        {
            _conferenciaParticipanteService.Deletar(codConferenciaParticipante);
        }
    }
}
