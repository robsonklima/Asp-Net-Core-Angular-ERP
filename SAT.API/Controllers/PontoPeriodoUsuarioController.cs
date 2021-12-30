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
    public class PontoPeriodoUsuarioController : ControllerBase
    {
        private readonly IPontoPeriodoUsuarioService _pontoPeriodoUsuarioService;

        public PontoPeriodoUsuarioController(IPontoPeriodoUsuarioService pontoPeriodoUsuarioService)
        {
            _pontoPeriodoUsuarioService = pontoPeriodoUsuarioService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoPeriodoUsuarioParameters parameters)
        {
            return _pontoPeriodoUsuarioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoUsuario}")]
        public PontoPeriodoUsuario Get(int codPontoPeriodoUsuario)
        {
            return _pontoPeriodoUsuarioService.ObterPorCodigo(codPontoPeriodoUsuario);
        }

        [HttpPost]
        public void Post([FromBody] PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Criar(pontoPeriodoUsuario: pontoPeriodoUsuario);
        }

        [HttpPut]
        public void Put([FromBody] PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Atualizar(pontoPeriodoUsuario: pontoPeriodoUsuario);
        }

        [HttpDelete("{codPontoPeriodoUsuario}")]
        public void Delete(int codPontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioService.Deletar(codPontoPeriodoUsuario);
        }
    }
}
