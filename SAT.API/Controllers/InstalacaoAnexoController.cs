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
    public class InstalacaoAnexoController : ControllerBase
    {
        private readonly IInstalacaoAnexoService _instalacaoAnexoService;

        public InstalacaoAnexoController(IInstalacaoAnexoService instalacaoAnexoService)
        {
            _instalacaoAnexoService = instalacaoAnexoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] InstalacaoAnexoParameters parameters)
        {
            return _instalacaoAnexoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalAnexo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public InstalacaoAnexo Get(int codInstalAnexo)
        {
            return _instalacaoAnexoService.ObterPorCodigo(codInstalAnexo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] InstalacaoAnexo instalacaoAnexo)
        {
            _instalacaoAnexoService.Criar(instalacaoAnexo);
        }

        [HttpDelete("{codInstalAnexo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codInstalAnexo)
        {
            _instalacaoAnexoService.Deletar(codInstalAnexo);
        }
    }
}