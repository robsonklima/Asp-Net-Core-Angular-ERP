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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class LaudoSituacaoController : ControllerBase
    {
        private ILaudoSituacaoService _laudoSituacaoService;
        public LaudoSituacaoController(ILaudoSituacaoService laudoSituacaoService)
        {
            _laudoSituacaoService = laudoSituacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] LaudoSituacaoParameters parameters)
        {
            return _laudoSituacaoService.ObterPorParametros(parameters);
        }
        
        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public LaudoSituacao Post([FromBody] LaudoSituacao laudoSituacao)
        {
            return _laudoSituacaoService.Criar(laudoSituacao);
        }

        [HttpGet("{codLaudoSituacao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public LaudoSituacao ObterPorCodigo(int codLaudoSituacao) =>
            _laudoSituacaoService.ObterPorCodigo(codLaudoSituacao);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] LaudoSituacao laudoSituacao)
        {
            _laudoSituacaoService.Atualizar(laudoSituacao);
        }

    }
}