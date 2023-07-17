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
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PerfilParameters parameters)
        {
            return _perfilService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPerfil}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Perfil Get(int codPerfil)
        {
            return _perfilService.ObterPorCodigo(codPerfil);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Perfil perfil)
        {
            _perfilService.Criar(perfil: perfil);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Perfil perfil)
        {
            _perfilService.Atualizar(perfil: perfil);
        }

        [HttpDelete("{codPerfil}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPerfil)
        {
            _perfilService.Deletar(codPerfil);
        }
    }
}
