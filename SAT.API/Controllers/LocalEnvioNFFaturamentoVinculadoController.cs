using System.Security.Claims;
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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] LocalEnvioNFFaturamentoVinculadoParameters parameters)
        {
            return _localEnvioNFFaturamentoVinculadoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodLocalEnvioNFFaturamento}/{CodPosto}/{CodContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public LocalEnvioNFFaturamentoVinculado Get(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            return _localEnvioNFFaturamentoVinculadoService.ObterPorCodigo(codLocalEnvioNFFaturamento, codPosto, codContrato);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public LocalEnvioNFFaturamentoVinculado Post([FromBody] LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            return _localEnvioNFFaturamentoVinculadoService.Criar(localEnvioNFFaturamentoVinculado);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            _localEnvioNFFaturamentoVinculadoService.Atualizar(localEnvioNFFaturamentoVinculado);
        }

        [HttpDelete("{CodLocalEnvioNffaturamento}/{codPosto}/{codContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            _localEnvioNFFaturamentoVinculadoService.Deletar(codLocalEnvioNFFaturamento, codPosto, codContrato);
        }
    }
}
