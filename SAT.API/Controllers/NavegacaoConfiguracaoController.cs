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
    public class NavegacaoConfiguracaoController : ControllerBase
    {
        private readonly INavegacaoConfiguracaoService _navegacaoConfiguracaoService;

        public NavegacaoConfiguracaoController(INavegacaoConfiguracaoService navegacaoConfiguracaoService)
        {
            _navegacaoConfiguracaoService = navegacaoConfiguracaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] NavegacaoConfiguracaoParameters parameters)
        {
            return _navegacaoConfiguracaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codNavegacaoConfiguracao}")]
        public NavegacaoConfiguracao Get(int codNavegacaoConfiguracao)
        {
            return _navegacaoConfiguracaoService.ObterPorCodigo(codNavegacaoConfiguracao);
        }

        [HttpPost]
        public void Post([FromBody] NavegacaoConfiguracao navegacaoConfiguracao)
        {
            _navegacaoConfiguracaoService.Criar(navegacaoConfiguracao: navegacaoConfiguracao);
        }

        [HttpPut]
        public void Put([FromBody] NavegacaoConfiguracao navegacaoConfiguracao)
        {
            _navegacaoConfiguracaoService.Atualizar(navegacaoConfiguracao: navegacaoConfiguracao);
        }

        [HttpDelete("{codNavegacaoConfiguracao}")]
        public void Delete(int codNavegacaoConfiguracao)
        {
            _navegacaoConfiguracaoService.Deletar(codNavegacaoConfiguracao);
        }
    }
}
