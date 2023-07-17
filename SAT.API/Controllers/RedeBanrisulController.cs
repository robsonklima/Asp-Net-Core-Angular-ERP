using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RedeBanrisulController : ControllerBase
    {
        private IRedeBanrisulService _RedeBanrisulService;

        public RedeBanrisulController(
            IRedeBanrisulService RedeBanrisulService
        )
        {
            _RedeBanrisulService = RedeBanrisulService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] RedeBanrisulParameters parameters)
        {
            return _RedeBanrisulService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRedeBanrisul}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public RedeBanrisul Get(int codRedeBanrisul)
        {
            return _RedeBanrisulService.ObterPorCodigo(codRedeBanrisul);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public RedeBanrisul Post([FromBody] RedeBanrisul rede)
        {
            return _RedeBanrisulService.Criar(rede);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public RedeBanrisul Put([FromBody] RedeBanrisul RedeBanrisul)
        {
            return _RedeBanrisulService.Atualizar(RedeBanrisul);
        }

        [HttpDelete("{codRedeBanrisul}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public RedeBanrisul Delete(int codRedeBanrisul)
        {
            return _RedeBanrisulService.Deletar(codRedeBanrisul);
        }
    }
}
