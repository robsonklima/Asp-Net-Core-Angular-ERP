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
    public class PontoUsuarioController : ControllerBase
    {
        private readonly IPontoUsuarioService _pontoUsuarioService;

        public PontoUsuarioController(IPontoUsuarioService pontoUsuarioService)
        {
            _pontoUsuarioService = pontoUsuarioService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoUsuarioParameters parameters)
        {
            return _pontoUsuarioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuario}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoUsuario Get(int codPontoUsuario)
        {
            return _pontoUsuarioService.ObterPorCodigo(codPontoUsuario);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public PontoUsuario Post([FromBody] PontoUsuario pontoUsuario)
        {
            return _pontoUsuarioService.Criar(pontoUsuario: pontoUsuario);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoUsuario pontoUsuario)
        {
            _pontoUsuarioService.Atualizar(pontoUsuario: pontoUsuario);
        }

        [HttpDelete("{codPontoUsuario}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoUsuario)
        {
            _pontoUsuarioService.Deletar(codPontoUsuario);
        }
    }
}
