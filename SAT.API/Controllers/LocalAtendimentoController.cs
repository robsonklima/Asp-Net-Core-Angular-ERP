using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] LocalAtendimentoParameters parameters)
        {
            return _localAtendimentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPosto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public LocalAtendimento Get(int codPosto)
        {
            return _localAtendimentoService.ObterPorCodigo(codPosto);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public LocalAtendimento Post([FromBody] LocalAtendimento localAtendimento)
        {
            return _localAtendimentoService.Criar(localAtendimento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] LocalAtendimento localAtendimento)
        {
            _localAtendimentoService.Atualizar(localAtendimento);
        }

        [HttpDelete("{codPosto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPosto)
        {
            _localAtendimentoService.Deletar(codPosto);
        }
    }
}
