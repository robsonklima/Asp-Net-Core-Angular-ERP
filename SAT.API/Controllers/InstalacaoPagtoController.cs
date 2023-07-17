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
    public class InstalacaoPagtoController : ControllerBase
    {
        private readonly IInstalacaoPagtoService _instalacaoPagtoService;

        public InstalacaoPagtoController(
            IInstalacaoPagtoService instalacaoPagtoService
        )
        {
            _instalacaoPagtoService = instalacaoPagtoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] InstalacaoPagtoParameters parameters)
        {
            return _instalacaoPagtoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codInstalPagto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public InstalacaoPagto Get(int codInstalPagto)
        {
            return _instalacaoPagtoService.ObterPorCodigo(codInstalPagto);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public InstalacaoPagto Post([FromBody] InstalacaoPagto instalacaoPagto)
        {
            return _instalacaoPagtoService.Criar(instalacaoPagto);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] InstalacaoPagto instalacaoPagto)
        {
            _instalacaoPagtoService.Atualizar(instalacaoPagto);
        }

        [HttpDelete("{codInstalPagto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codInstalPagto)
        {
            _instalacaoPagtoService.Deletar(codInstalPagto);
        }
    }
}
