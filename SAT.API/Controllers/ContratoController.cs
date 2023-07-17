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
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoInterface;

        public ContratoController(IContratoService contratoInterface)
        {
            _contratoInterface = contratoInterface;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ContratoParameters parameters)
        {
            return _contratoInterface.ObterPorParametros(parameters);
        }

        [HttpGet("{codContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Contrato Get(int codContrato)
        {
            return _contratoInterface.ObterPorCodigo(codContrato);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Contrato Post([FromBody] Contrato contrato)
        {
            return _contratoInterface.Criar(contrato);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public Contrato Put([FromBody] Contrato contrato)
        {
            return _contratoInterface.Atualizar(contrato);
        }

        [HttpDelete("{codContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codContrato)
        {
            throw new System.NotImplementedException("DELETAR NÃO IMPLEMENTADO");
        }
    }
}
