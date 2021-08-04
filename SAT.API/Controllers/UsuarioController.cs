using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioInterface;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public UsuarioController(
            IUsuarioRepository usuarioInterface,
            IConfiguration config,
            ITokenService tokenService
        )
        {
            _usuarioInterface = usuarioInterface;
            _config = config;
            _tokenService = tokenService;
        }

        [HttpGet]
        public UsuarioListViewModel Get([FromQuery] UsuarioParameters parameters)
        {
            var usuarios = _usuarioInterface.ObterPorParametros(parameters);

            var usuarioListViewModel = new UsuarioListViewModel
            {
                Usuarios = usuarios,
                TotalCount = usuarios.TotalCount,
                CurrentPage = usuarios.CurrentPage,
                PageSize = usuarios.PageSize,
                TotalPages = usuarios.TotalPages,
                HasNext = usuarios.HasNext,
                HasPrevious = usuarios.HasPrevious
            };

            return usuarioListViewModel;
        }

        [HttpGet("{codUsuario}")]
        public Usuario Get(string codUsuario)
        {
            return _usuarioInterface.ObterPorCodigo(codUsuario);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public UsuarioLoginViewModel Post([FromBody] Usuario usuario)
        {
            var usuarioLogado = _usuarioInterface.Login(usuario: usuario);

            for (int i = 0; i < usuarioLogado.Perfil.NavegacoesConfiguracao.Count; i++)
            {
                usuarioLogado.Perfil.NavegacoesConfiguracao.ToArray()[i].Navegacao.Id = usuarioLogado.Perfil.NavegacoesConfiguracao.ToArray()[i].Navegacao.Title.ToLower();
            }

            var navegacoes = usuarioLogado.Perfil?.NavegacoesConfiguracao.Select(n => n.Navegacao).Where(n => n.CodNavegacaoPai == null).OrderBy(n => n.Ordem).ToList();
            usuarioLogado.Perfil.NavegacoesConfiguracao = null;
            var token = _tokenService.GerarToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), usuarioLogado);

            return new UsuarioLoginViewModel()
            {
                Usuario = usuarioLogado,
                Navegacoes = navegacoes,
                Token = token
            };
        }
    }
}
