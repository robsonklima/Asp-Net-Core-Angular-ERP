using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private IRegiaoService _regiaoService;

        public RegiaoController(
            IRegiaoService regiaoService
        )
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] RegiaoParameters parameters)
        {
            return _regiaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRegiao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Regiao Get(int codRegiao)
        {
            return _regiaoService.ObterPorCodigo(codRegiao);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Regiao regiao)
        {
            _regiaoService.Criar(regiao: regiao);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Regiao regiao)
        {
            _regiaoService.Atualizar(regiao);
        }

        [HttpDelete("{codRegiao}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codRegiao)
        {
            _regiaoService.Deletar(codRegiao);
        }
    }
}
