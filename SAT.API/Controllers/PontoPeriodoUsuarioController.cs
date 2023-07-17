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
    public class PontoPeriodoUsuarioController : ControllerBase
    {
        private readonly IPontoPeriodoUsuarioService _pontoPeriodoUsuarioService;

        public PontoPeriodoUsuarioController(IPontoPeriodoUsuarioService pontoPeriodoUsuarioService)
        {
            _pontoPeriodoUsuarioService = pontoPeriodoUsuarioService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoPeriodoUsuarioParameters parameters)
        {
            return _pontoPeriodoUsuarioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoUsuario}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoPeriodoUsuario Get(int codPontoPeriodoUsuario)
        {
            return _pontoPeriodoUsuarioService.ObterPorCodigo(codPontoPeriodoUsuario);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Criar(pontoPeriodoUsuario: pontoPeriodoUsuario);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Atualizar(pontoPeriodoUsuario: pontoPeriodoUsuario);
        }

        [HttpDelete("{codPontoPeriodoUsuario}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Deletar(codPontoPeriodoUsuario);
        }
    }
}
