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
    public class MotivoCancelamentoController : ControllerBase
    {
        private IMotivoCancelamentoService _MotivoCancelamentoService;

        public MotivoCancelamentoController(
            IMotivoCancelamentoService MotivoCancelamentoService
        )
        {
            _MotivoCancelamentoService = MotivoCancelamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] MotivoCancelamentoParameters parameters)
        {
            return _MotivoCancelamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMotivoCancelamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public MotivoCancelamento Get(int codMotivoCancelamento)
        {
            return _MotivoCancelamentoService.ObterPorCodigo(codMotivoCancelamento);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public MotivoCancelamento Post([FromBody] MotivoCancelamento op)
        {
            return _MotivoCancelamentoService.Criar(op);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public MotivoCancelamento Put([FromBody] MotivoCancelamento op)
        {
            return _MotivoCancelamentoService.Atualizar(op);
        }

        [HttpDelete("{codMotivoCancelamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public MotivoCancelamento Delete(int codMotivoCancelamento)
        {
            return _MotivoCancelamentoService.Deletar(codMotivoCancelamento);
        }
    }
}
