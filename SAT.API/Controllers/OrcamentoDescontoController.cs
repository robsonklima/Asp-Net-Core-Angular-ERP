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
    public class OrcamentoDescontoController : ControllerBase
    {
        private readonly IOrcamentoDescontoService _orcamentoDescService;

        public OrcamentoDescontoController(IOrcamentoDescontoService orcamentoDescService)
        {
            _orcamentoDescService = orcamentoDescService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcamentoDescontoParameters parameters) =>
            _orcamentoDescService.ObterPorParametros(parameters);

        [HttpGet("{codOrcamentoDesc}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OrcamentoDesconto Get(int codOrcamentoDesc) =>
            _orcamentoDescService.ObterPorCodigo(codOrcamentoDesc);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OrcamentoDesconto Post([FromBody] OrcamentoDesconto desconto) =>
            _orcamentoDescService.Criar(desconto);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public OrcamentoDesconto Put([FromBody] OrcamentoDesconto desconto) =>
            _orcamentoDescService.Atualizar(desconto);

        [HttpDelete("{codOrcamentoDesc}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOrcamentoDesc) =>
            _orcamentoDescService.Deletar(codOrcamentoDesc);
    }
}