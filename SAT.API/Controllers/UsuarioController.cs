using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] UsuarioParameters parameters)
        {
            return _usuarioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codUsuario}")]
        public Usuario Get(string codUsuario)
        {
            return _usuarioService.ObterPorCodigo(codUsuario);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public UsuarioLoginViewModel Post([FromBody] Usuario usuario)
        {
            return _usuarioService.Login(usuario);
        }
    }
}
