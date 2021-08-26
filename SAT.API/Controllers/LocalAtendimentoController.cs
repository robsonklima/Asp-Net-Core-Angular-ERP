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
    public class LocalAtendimentoController : ControllerBase
    {
        private readonly ILocalAtendimentoService _localAtendimentoService;

        public LocalAtendimentoController(
            ILocalAtendimentoService localAtendimentoService
        )
        {
            _localAtendimentoService = localAtendimentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] LocalAtendimentoParameters parameters)
        {
            return _localAtendimentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPosto}")]
        public LocalAtendimento Get(int codPosto)
        {
            return _localAtendimentoService.ObterPorCodigo(codPosto);
        }

        [HttpPost]
        public LocalAtendimento Post([FromBody] LocalAtendimento localAtendimento)
        {
            return _localAtendimentoService.Criar(localAtendimento);
        }

        [HttpPut]
        public void Put([FromBody] LocalAtendimento localAtendimento)
        {
            _localAtendimentoService.Atualizar(localAtendimento);
        }

        [HttpDelete("{codPosto}")]
        public void Delete(int codPosto)
        {
            _localAtendimentoService.Deletar(codPosto);
        }
    }
}
