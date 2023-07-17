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
    public class ORSolucaoController : ControllerBase
    {
        private readonly IORSolucaoService _orSolucaoService;

        public ORSolucaoController(IORSolucaoService orSolucaoService)
        {
            _orSolucaoService = orSolucaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORSolucaoParameters parameters)
        {
            return _orSolucaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codSolucao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORSolucao Get(int codSolucao)
        {
            return _orSolucaoService.ObterPorCodigo(codSolucao);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORSolucao orSolucao)
        {
            _orSolucaoService.Criar(orSolucao);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORSolucao orSolucao)
        {
            _orSolucaoService.Atualizar(orSolucao);
        }

        [HttpDelete("{codSolucao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codSolucao)
        {
            _orSolucaoService.Deletar(codSolucao);
        }
    }
}
