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
    public class PontoUsuarioController : ControllerBase
    {
        private readonly IPontoUsuarioService _pontoUsuarioService;

        public PontoUsuarioController(IPontoUsuarioService pontoUsuarioService)
        {
            _pontoUsuarioService = pontoUsuarioService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoUsuarioParameters parameters)
        {
            return _pontoUsuarioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuario}")]
        public PontoUsuario Get(int codPontoUsuario)
        {
            return _pontoUsuarioService.ObterPorCodigo(codPontoUsuario);
        }

        [HttpPost]
        public void Post([FromBody] PontoUsuario pontoUsuario)
        {
            _pontoUsuarioService.Criar(pontoUsuario: pontoUsuario);
        }

        [HttpPut]
        public void Put([FromBody] PontoUsuario pontoUsuario)
        {
            _pontoUsuarioService.Atualizar(pontoUsuario: pontoUsuario);
        }

        [HttpDelete("{codPontoUsuario}")]
        public void Delete(int codPontoUsuario)
        {
            _pontoUsuarioService.Deletar(codPontoUsuario);
        }
    }
}
