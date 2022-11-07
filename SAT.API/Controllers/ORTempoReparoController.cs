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
    public class ORTempoReparoController : ControllerBase
    {
        private readonly IORTempoReparoService _orTempoReparoService;

        public ORTempoReparoController(IORTempoReparoService orTempoReparoService)
        {
            _orTempoReparoService = orTempoReparoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ORTempoReparoParameters parameters)
        {
            return _orTempoReparoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codORTempoReparo}")]
        public ORTempoReparo Get(int codORTempoReparo)
        {
            return _orTempoReparoService.ObterPorCodigo(codORTempoReparo);
        }

        [HttpPost]
        public ORTempoReparo Post([FromBody] ORTempoReparo tr)
        {
            return _orTempoReparoService.Criar(tr);
        }

        [HttpPut]
        public ORTempoReparo Put([FromBody] ORTempoReparo tr)
        {
            return _orTempoReparoService.Atualizar(tr);
        }

        [HttpDelete("{codORTempoReparo}")]
        public void Delete(int codORTempoReparo)
        {
            _orTempoReparoService.Deletar(codORTempoReparo);
        }
    }
}
