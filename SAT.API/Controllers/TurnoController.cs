using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
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
        public ListViewModel Get([FromQuery] TurnoParameters parameters)
        {
            return _turnoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTurno}")]
        public Turno Get(int codTurno)
        {
            return _turnoService.ObterPorCodigo(codTurno);
        }

        [HttpPost]
        [Authorize(Roles = "44")]
        public void Post([FromBody] Turno turno)
        {
            _turnoService.Criar(turno);
        }

        [HttpPut]
        [Authorize(Roles = "44")]
        public void Put([FromBody] Turno turno)
        {
            _turnoService.Atualizar(turno);
        }

        [HttpDelete("{codTurno}")]
        public void Delete(int codTurno)
        {
            _turnoService.Deletar(codTurno);
        }
    }
}
