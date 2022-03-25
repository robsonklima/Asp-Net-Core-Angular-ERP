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
    public class LiderTecnicoController : ControllerBase
    {
        private readonly ILiderTecnicoService _LiderTecnicoService;

        public LiderTecnicoController(ILiderTecnicoService LiderTecnicoService)
        {
            _LiderTecnicoService = LiderTecnicoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] LiderTecnicoParameters parameters)
        {
            return _LiderTecnicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codLiderTecnico}")]
        public LiderTecnico Get(int codLiderTecnico)
        {
            return _LiderTecnicoService.ObterPorCodigo(codLiderTecnico);
        }

        [HttpPost]
        public LiderTecnico Post([FromBody] LiderTecnico LiderTecnico)
        {
            return _LiderTecnicoService.Criar(LiderTecnico);
        }

        [HttpPut]
        public void Put([FromBody] LiderTecnico LiderTecnico)
        {
            _LiderTecnicoService.Atualizar(LiderTecnico);
        }

        [HttpDelete("{codLiderTecnico}")]
        public void Delete(int codLiderTecnico)
        {
            _LiderTecnicoService.Deletar(codLiderTecnico);
        }
    }
}
