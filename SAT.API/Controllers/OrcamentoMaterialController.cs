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
    public class OrcamentoMaterialController : ControllerBase
    {
        private readonly IOrcamentoMaterialService _orcamentoMatService;

        public OrcamentoMaterialController(IOrcamentoMaterialService orcamentoMatService)
        {
            _orcamentoMatService = orcamentoMatService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcamentoMaterialParameters parameters) =>
            _orcamentoMatService.ObterPorParametros(parameters);

        [HttpGet("{codOrcMaterial}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OrcamentoMaterial Get(int codOrcMaterial) =>
            _orcamentoMatService.ObterPorCodigo(codOrcMaterial);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OrcamentoMaterial Post([FromBody] OrcamentoMaterial orcamentoMat) =>
            _orcamentoMatService.Criar(orcamentoMat);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public OrcamentoMaterial Put([FromBody] OrcamentoMaterial orcamentoMat) =>
            _orcamentoMatService.Atualizar(orcamentoMat);

        [HttpDelete("{codOrcMaterial}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcMaterial) =>
            _orcamentoMatService.Deletar(codOrcMaterial);
    }
}