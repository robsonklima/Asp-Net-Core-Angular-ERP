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
    public class LocalEnvioNFFaturamentoVinculadoController : ControllerBase
    {
        private readonly ILocalEnvioNFFaturamentoVinculadoService _localEnvioNFFaturamentoVinculadoService;

        public LocalEnvioNFFaturamentoVinculadoController(
            ILocalEnvioNFFaturamentoVinculadoService localEnvioNFFaturamentoVinculadoService
        )
        {
            _localEnvioNFFaturamentoVinculadoService = localEnvioNFFaturamentoVinculadoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] LocalEnvioNFFaturamentoVinculadoParameters parameters)
        {
            return _localEnvioNFFaturamentoVinculadoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodLocalEnvioNffaturamento}")]
        public LocalEnvioNFFaturamentoVinculado Get(int CodLocalEnvioNffaturamento)
        {
            return _localEnvioNFFaturamentoVinculadoService.ObterPorCodigo(CodLocalEnvioNffaturamento);
        }

        [HttpPost]
        public LocalEnvioNFFaturamentoVinculado Post([FromBody] LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            return _localEnvioNFFaturamentoVinculadoService.Criar(localEnvioNFFaturamentoVinculado);
        }

        [HttpPut]
        public void Put([FromBody] LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            _localEnvioNFFaturamentoVinculadoService.Atualizar(localEnvioNFFaturamentoVinculado);
        }

        [HttpDelete("{CodLocalEnvioNffaturamento}")]
        public void Delete(int CodLocalEnvioNffaturamento)
        {
            _localEnvioNFFaturamentoVinculadoService.Deletar(CodLocalEnvioNffaturamento);
        }
    }
}
