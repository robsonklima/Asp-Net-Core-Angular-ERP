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

        [HttpGet("{CodLocalEnvioNFFaturamento}/{CodPosto}/{CodContrato}")]
        public LocalEnvioNFFaturamentoVinculado Get(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            return _localEnvioNFFaturamentoVinculadoService.ObterPorCodigo(codLocalEnvioNFFaturamento, codPosto, codContrato);
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

        [HttpDelete("{CodLocalEnvioNffaturamento}/{codPosto}/{codContrato}")]
        public void Delete(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            _localEnvioNFFaturamentoVinculadoService.Deletar(codLocalEnvioNFFaturamento, codPosto, codContrato);
        }
    }
}
