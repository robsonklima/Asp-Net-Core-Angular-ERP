using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
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

        public UsuarioDispositivoController(
            IUsuarioDispositivoService usuarioDispositivoService,
            IEmailService emailService
        )
        {
            _usuarioDispositivoService = usuarioDispositivoService;
            _emailService = emailService;
        }

        [HttpGet("{codUsuario}/{hash}")]
        public UsuarioDispositivo Get(string codUsuario, string hash)
        {
            return _usuarioDispositivoService.ObterPorUsuarioEHash(codUsuario, hash);
        }

        [HttpPost]
        public void Post([FromBody] UsuarioDispositivo hash)
        {
            _usuarioDispositivoService.Criar(hash);
        }
    }
}
