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

        [AllowAnonymous]
        [HttpGet("{codUsuario}")]
        public Usuario Get(string codUsuario)
        {
            return _usuarioService.ObterPorCodigo(codUsuario);
        }

        [HttpPut]
        [Route("Criar")]
        public void Criar([FromBody] Usuario usuario)
        {
            _usuarioService.Criar(usuario);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public UsuarioLoginViewModel Post([FromBody] Usuario usuario)
        {
            return _usuarioService.Login(usuario);
        }


        [HttpPut]
        public void Put([FromBody] Usuario usuario)
        {
            _usuarioService.Atualizar(usuario);
        }

        [HttpPut]
        [Route("AlterarSenha")]
        public void AlterarSenha([FromBody] SegurancaUsuarioModel segurancaUsuarioModel)
        {
            _usuarioService.AlterarSenha(segurancaUsuarioModel);
        }

        [AllowAnonymous]
        [HttpPost("EsqueceuSenha/{codUsuario}")]
        public IActionResult EsqueceuSenha([FromRoute] string codUsuario)
        {
            ResponseObject response = _usuarioService.EsqueceuSenha(codUsuario);
            return response.RequestValido ? Ok(response) : BadRequest(response);
        }
    }
}
