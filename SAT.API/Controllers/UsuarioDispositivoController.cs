using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioDispositivoController : ControllerBase
    {
        private readonly IUsuarioDispositivoService _usuarioDispositivoService;
        private readonly IEmailService _emailService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioDispositivoController(
            IUsuarioDispositivoService usuarioDispositivoService,
            IEmailService emailService,
            IUsuarioService usuarioService
        )
        {
            _usuarioDispositivoService = usuarioDispositivoService;
            _emailService = emailService;
            _usuarioService = usuarioService;
        }

        [HttpGet("{codUsuario}/{hash}")]
        public UsuarioDispositivo Get(string codUsuario, string hash)
        {
            return _usuarioDispositivoService.ObterPorUsuarioEHash(codUsuario, hash);
        }

        [HttpPost]
        public void Post([FromBody] UsuarioDispositivo dispositivo)
        {
            var usuario = _usuarioService.ObterPorCodigo(dispositivo.CodUsuario);

            if (!string.IsNullOrWhiteSpace(usuario.Email)) {
                var email = new Email() {
                    NomeRemetente = Constants.SISTEMA_NOME,
                    EmailRemetente = Constants.SISTEMA_EMAIL,
                    NomeDestinatario = usuario.NomeUsuario,
                    EmailDestinatario = usuario.Email,
                    Assunto = Constants.SISTEMA_ATIVACAO_ACESSO,
                    Corpo = "Teste"
                };

                _emailService.Enviar(email);
            }

            _usuarioDispositivoService.Criar(dispositivo);
        }
    }
}
