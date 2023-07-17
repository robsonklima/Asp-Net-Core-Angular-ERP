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
    public class IntegracaoCobraController : ControllerBase
    {
        private readonly IIntegracaoCobraService _integracaoCobraService;

        public IntegracaoCobraController(IIntegracaoCobraService integracaoCobraService)
        {
            _integracaoCobraService = integracaoCobraService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] IntegracaoCobraParameters parameters)
        {
            return _integracaoCobraService.ObterPorParametros(parameters);
        }

        [HttpGet("{codIntegracaoCobra}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public IntegracaoCobra Get(int codIntegracaoCobra)
        {
            return _integracaoCobraService.ObterPorCodigo(codIntegracaoCobra);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public IntegracaoCobra Post([FromBody] IntegracaoCobra integracaoCobra)
        {
            return _integracaoCobraService.Criar(integracaoCobra);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] IntegracaoCobra integracaoCobra)
        {
            _integracaoCobraService.Atualizar(integracaoCobra);
        }

        [HttpDelete("{codIntegracaoCobra}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codIntegracaoCobra)
        {
            _integracaoCobraService.Deletar(codIntegracaoCobra);
        }
    }
}
