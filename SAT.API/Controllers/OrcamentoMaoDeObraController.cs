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
    public class OrcamentoMaoDeObraController : ControllerBase
    {
        private readonly IOrcamentoMaoDeObraService _orcamentoMaoDeObraService;

        public OrcamentoMaoDeObraController(IOrcamentoMaoDeObraService orcamentoMaoDeObraService)
        {
            _orcamentoMaoDeObraService = orcamentoMaoDeObraService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcamentoMaoDeObraParameters parameters) =>
            _orcamentoMaoDeObraService.ObterPorParametros(parameters);

        [HttpGet("{codOrcamentoMaoDeObra}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OrcamentoMaoDeObra Get(int codOrcamentoMaoDeObra) =>
            _orcamentoMaoDeObraService.ObterPorCodigo(codOrcamentoMaoDeObra);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OrcamentoMaoDeObra Post([FromBody] OrcamentoMaoDeObra orcamentoMaoDeObra) =>
            _orcamentoMaoDeObraService.Criar(orcamentoMaoDeObra);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public OrcamentoMaoDeObra Put([FromBody] OrcamentoMaoDeObra orcamentoMaoDeObra) =>
            _orcamentoMaoDeObraService.Atualizar(orcamentoMaoDeObra);

        [HttpDelete("{codOrcamentoMaoDeObra}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcamentoMaoDeObra) =>
            _orcamentoMaoDeObraService.Deletar(codOrcamentoMaoDeObra);
    }
}