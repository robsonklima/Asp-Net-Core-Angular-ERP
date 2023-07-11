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
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TurnoParameters parameters)
        {
            return _turnoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTurno}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Turno Get(int codTurno)
        {
            return _turnoService.ObterPorCodigo(codTurno);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Turno turno)
        {
            _turnoService.Criar(turno);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Turno turno)
        {
            _turnoService.Atualizar(turno);
        }

        [HttpDelete("{codTurno}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTurno)
        {
            _turnoService.Deletar(codTurno);
        }
    }
}
