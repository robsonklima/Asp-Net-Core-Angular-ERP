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
    public class OrcamentoMotivoController : ControllerBase
    {
        private readonly IOrcamentoMotivoService _orcamentoMotivoService;

        public OrcamentoMotivoController(IOrcamentoMotivoService orcamentoMotivoService)
        {
            _orcamentoMotivoService = orcamentoMotivoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcamentoMotivoParameters parameters) =>
            _orcamentoMotivoService.ObterPorParametros(parameters);

        [HttpGet("{codOrcMotivo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OrcamentoMotivo Get(int codOrcMotivo) =>
            _orcamentoMotivoService.ObterPorCodigo(codOrcMotivo);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OrcamentoMotivo Post([FromBody] OrcamentoMotivo orcamentoMotivo) =>
            _orcamentoMotivoService.Criar(orcamentoMotivo);

        [HttpPut("{codOrcMotivo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] OrcamentoMotivo orcamentoMotivo) =>
            _orcamentoMotivoService.Atualizar(orcamentoMotivo);

        [HttpDelete("{codOrcMotivo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcMotivo) =>
            _orcamentoMotivoService.Deletar(codOrcMotivo);
    }
}
