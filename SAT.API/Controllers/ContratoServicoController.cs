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
    public class ContratoServicoController : ControllerBase
    {
        public IContratoServicoService _contratoServicoService { get; }

        public ContratoServicoController(IContratoServicoService contratoServicoService)
        {
            _contratoServicoService = contratoServicoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ContratoServicoParameters parameters)
        {
            return _contratoServicoService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodContratoServico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ContratoServico Get(int codContratoServico)
        {
            return _contratoServicoService.ObterPorCodigo(codContratoServico);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ContratoServico contratoServico)
        {
            _contratoServicoService.Criar(contratoServico);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ContratoServico contratoServico)
        {
            _contratoServicoService.Atualizar(contratoServico);
        }

        [HttpDelete("{CodContratoServico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codContratoServico)
        {
            _contratoServicoService.Deletar(codContratoServico);
        }
    }
}
