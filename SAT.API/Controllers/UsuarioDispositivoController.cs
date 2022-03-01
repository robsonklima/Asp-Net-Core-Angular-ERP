using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
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

        [HttpGet]
        public ListViewModel Get([FromQuery] UsuarioDispositivoParameters parameters)
        {
            return _usuarioDispositivoService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public UsuarioDispositivo Post([FromBody] UsuarioDispositivo dispositivo)
        {
            return _usuarioDispositivoService.Criar(dispositivo);
        }

        [HttpPut]
        public void Put([FromBody] UsuarioDispositivo dispositivo)
        {
            _usuarioDispositivoService.Atualizar(dispositivo);
        }

        [HttpGet("{codUsuarioDispositivo}")]
        public UsuarioDispositivo Get(int codUsuarioDispositivo)
        {
            return _usuarioDispositivoService.ObterPorCodigo(codUsuarioDispositivo);
        }
    }
}
