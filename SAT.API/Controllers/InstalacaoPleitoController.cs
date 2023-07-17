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
    public class InstalacaoPleitoController : ControllerBase
    {
        private readonly IInstalacaoPleitoService _instalacaoPleitoService;

        public InstalacaoPleitoController(
            IInstalacaoPleitoService instalacaoPleitoService
        )
        {
            _instalacaoPleitoService = instalacaoPleitoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] InstalacaoPleitoParameters parameters)
        {
            return _instalacaoPleitoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalPleito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public InstalacaoPleito Get(int codInstalPleito)
        {
            return _instalacaoPleitoService.ObterPorCodigo(codInstalPleito);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public InstalacaoPleito Post([FromBody] InstalacaoPleito instalacaoPleito)
        {
            return _instalacaoPleitoService.Criar(instalacaoPleito);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] InstalacaoPleito instalacaoPleito)
        {
            _instalacaoPleitoService.Atualizar(instalacaoPleito);
        }

        [HttpDelete("{CodInstalPleito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codInstalacaoPleito)
        {
            _instalacaoPleitoService.Deletar(codInstalacaoPleito);
        }
    }
}
