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
    public class AcordoNivelServicoController : ControllerBase
    {
        private IAcordoNivelServicoService _ansService;

        public AcordoNivelServicoController(IAcordoNivelServicoService ansService)
        {
            _ansService = ansService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AcordoNivelServicoParameters parameters)
        {
            return _ansService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public AcordoNivelServico Post([FromBody] AcordoNivelServico acordoNivelServico)
        {
            return _ansService.Criar(acordoNivelServico);
        }

        [HttpGet("{codSLA}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AcordoNivelServico Get(int codSLA)
        {
            return _ansService.ObterPorCodigo(codSLA);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AcordoNivelServico acordoNivelServico)
        {
            _ansService.Atualizar(acordoNivelServico);
        }

        [HttpDelete("{codSLA}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codSLA)
        {
            _ansService.Deletar(codSLA);
        }
    }
}
