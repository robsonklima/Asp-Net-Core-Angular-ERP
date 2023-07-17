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
    public class InstalacaoController : ControllerBase
    {
        private readonly IInstalacaoService _instalacaoService;

        public InstalacaoController(
            IInstalacaoService instalacaoService
        )
        {
            _instalacaoService = instalacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] InstalacaoParameters parameters)
        {
            return _instalacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Instalacao Get(int codInstalacao)
        {
            return _instalacaoService.ObterPorCodigo(codInstalacao);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Instalacao Post([FromBody] Instalacao instalacao)
        {
            return _instalacaoService.Criar(instalacao);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Instalacao instalacao)
        {
            _instalacaoService.Atualizar(instalacao);
        }

        [HttpDelete("{CodInstalacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codInstalacao)
        {
            _instalacaoService.Deletar(codInstalacao);
        }
    }
}
