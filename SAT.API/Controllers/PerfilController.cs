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
        public ListViewModel Get([FromQuery] PerfilParameters parameters)
        {
            return _perfilService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPerfil}")]
        public Perfil Get(int codPerfil)
        {
            return _perfilService.ObterPorCodigo(codPerfil);
        }

        [HttpPost]
        public void Post([FromBody] Perfil perfil)
        {
            _perfilService.Criar(perfil: perfil);
        }

        [HttpPut]
        public void Put([FromBody] Perfil perfil)
        {
            _perfilService.Atualizar(perfil: perfil);
        }

        [HttpDelete("{codPerfil}")]
        public void Delete(int codPerfil)
        {
            _perfilService.Deletar(codPerfil);
        }
    }
}
