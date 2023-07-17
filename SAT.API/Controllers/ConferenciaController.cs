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
    public class ConferenciaController : ControllerBase
    {
        private readonly IConferenciaService _conferenciaService;

        public ConferenciaController(IConferenciaService conferenciaService)
        {
            _conferenciaService = conferenciaService;
        }

        [HttpGet("{codConferencia}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Conferencia Get(int codConferencia)
        {
            return _conferenciaService.ObterPorCodigo(codConferencia);
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ConferenciaParameters parameters)
        {
            return _conferenciaService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Conferencia Post([FromBody] Conferencia Conferencia)
        {
            return _conferenciaService.Criar(Conferencia);
        }

        [HttpDelete("{codConferencia}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codConferencia)
        {
            _conferenciaService.Deletar(codConferencia);
        }
    }
}
