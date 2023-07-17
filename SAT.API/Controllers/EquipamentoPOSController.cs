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
    public class EquipamentoPOSController : ControllerBase
    {
        private IEquipamentoPOSService _EquipamentoPOSService;

        public EquipamentoPOSController(
            IEquipamentoPOSService EquipamentoPOSService
        )
        {
            _EquipamentoPOSService = EquipamentoPOSService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] EquipamentoPOSParameters parameters)
        {
            return _EquipamentoPOSService.ObterPorParametros(parameters);
        }

        [HttpGet("{codEquipamentoPOS}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public EquipamentoPOS Get(int codEquipamentoPOS)
        {
            return _EquipamentoPOSService.ObterPorCodigo(codEquipamentoPOS);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public EquipamentoPOS Post([FromBody] EquipamentoPOS op)
        {
            return _EquipamentoPOSService.Criar(op);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public EquipamentoPOS Put([FromBody] EquipamentoPOS op)
        {
            return _EquipamentoPOSService.Atualizar(op);
        }

        [HttpDelete("{codEquipamentoPOS}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public EquipamentoPOS Delete(int codEquipamentoPOS)
        {
            return _EquipamentoPOSService.Deletar(codEquipamentoPOS);
        }
    }
}
