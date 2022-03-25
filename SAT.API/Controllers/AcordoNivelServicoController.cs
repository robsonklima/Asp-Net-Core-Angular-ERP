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
    public class AcordoNivelServicoController : ControllerBase
    {
        private IAcordoNivelServicoService _ansService;

        public AcordoNivelServicoController(IAcordoNivelServicoService ansService)
        {
            _ansService = ansService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AcordoNivelServicoParameters parameters)
        {
            return _ansService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public AcordoNivelServico Post([FromBody] AcordoNivelServico acordoNivelServico)
        {
            return _ansService.Criar(acordoNivelServico);
        }

        [HttpGet("{codSLA}")]
        public AcordoNivelServico Get(int codSLA)
        {
            return _ansService.ObterPorCodigo(codSLA);
        }

        [HttpPut]
        public void Put([FromBody] AcordoNivelServico acordoNivelServico)
        {
            _ansService.Atualizar(acordoNivelServico);
        }

        [HttpDelete("{codSLA}")]
        public void Delete(int codSLA)
        {
            _ansService.Deletar(codSLA);
        }
    }
}
