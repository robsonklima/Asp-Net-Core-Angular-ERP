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
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentoController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] EquipamentoParameters parameters)
        {
            return _equipamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Equipamento Get(int codEquip)
        {
            return _equipamentoService.ObterPorCodigo(codEquip);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Equipamento equipamento)
        {
            this._equipamentoService.Criar(equipamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Equipamento equipamento)
        {
            this._equipamentoService.Atualizar(equipamento);
        }

        [HttpDelete("{codEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codEquip)
        {
        }
    }
}
