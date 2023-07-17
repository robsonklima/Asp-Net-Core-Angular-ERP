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
    public class ContratoEquipamentoController : ControllerBase
    {
        private readonly IContratoEquipamentoService _contratoEquipamentoInterface;

        public ContratoEquipamentoController(IContratoEquipamentoService contratoEquipamentoInterface)
        {
            _contratoEquipamentoInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ContratoEquipamentoParameters parameters)
        {
            return _contratoEquipamentoInterface.ObterPorParametros(parameters);
        }


        [HttpGet("{codContratoEquipamento}/{codEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ContratoEquipamento Get(int codContratoEquipamento, int codEquip)
        {
            return _contratoEquipamentoInterface.ObterPorCodigo(codContratoEquipamento, codEquip);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ContratoEquipamento contratoEquipamento)
        {
            _contratoEquipamentoInterface.Criar(contratoEquipamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ContratoEquipamento contratoEquipamento)
        {
            _contratoEquipamentoInterface.Atualizar(contratoEquipamento);
        }

        [HttpDelete("{codContrato}/{codEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codContrato, int codEquip)
        {
            _contratoEquipamentoInterface.Deletar(codContrato,codEquip);
        }
    }
}
