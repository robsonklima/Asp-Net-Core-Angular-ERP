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
    public class EquipamentoContratoController : ControllerBase
    {
        private readonly IEquipamentoContratoService _equipamentoContratoService;

        public EquipamentoContratoController(
            IEquipamentoContratoService equipamentoContratoService
        )
        {
            _equipamentoContratoService = equipamentoContratoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] EquipamentoContratoParameters parameters)
        {
            return _equipamentoContratoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codEquipContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public EquipamentoContrato Get(int codEquipContrato)
        {
            return _equipamentoContratoService.ObterPorCodigo(codEquipContrato);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        {
            return _equipamentoContratoService.Criar(equipamentoContrato);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] EquipamentoContrato equipamentoContrato)
        {
            _equipamentoContratoService.Atualizar(equipamentoContrato);
        }

        [HttpDelete("{codEquipContrato}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codEquipContrato)
        {
            _equipamentoContratoService.Deletar(codEquipContrato);
        }
    }
}
