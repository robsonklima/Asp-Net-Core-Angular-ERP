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

        public UsuarioDispositivoController(IUsuarioDispositivoService usuarioDispositivoService)
        {
            _usuarioDispositivoService = usuarioDispositivoService;
        }

        [HttpGet("{codUsuario}/{hash}")]
        public UsuarioDispositivo Get(string codUsuario, string hash)
        {
            return _usuarioDispositivoService.ObterPorUsuarioEHash(codUsuario, hash);
        }

        [HttpPost]
        public void Post([FromBody] UsuarioDispositivo dispositivo)
        {
            _usuarioDispositivoService.Criar(dispositivo);
        }

        [HttpPut]
        public void Put([FromBody] UsuarioDispositivo dispositivo)
        {
            _usuarioDispositivoService.Atualizar(dispositivo);
        }
    }
}
