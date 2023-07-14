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
    public class NavegacaoController : ControllerBase
    {
        private readonly INavegacaoService _navegacaoService;

        public NavegacaoController(INavegacaoService navegacaoService)
        {
            _navegacaoService = navegacaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] NavegacaoParameters parameters)
        {
            return _navegacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codNavegacao}")]
        public Navegacao Get(int codNavegacao)
        {
            return _navegacaoService.ObterPorCodigo(codNavegacao);
        }

        [HttpPost]
        public void Post([FromBody] Navegacao navegacao)
        {
            _navegacaoService.Criar(navegacao);
        }

        [HttpPut]
        public void Put([FromBody] Navegacao navegacao)
        {
            _navegacaoService.Atualizar(navegacao);
        }

        [HttpDelete("{codNavegacao}")]
        public void Delete(int codNavegacao)
        {
            _navegacaoService.Deletar(codNavegacao);
        }
    }
}
