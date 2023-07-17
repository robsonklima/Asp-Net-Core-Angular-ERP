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
    public class DefeitoPOSController : ControllerBase
    {
        private IDefeitoPOSService _DefeitoPOSService;

        public DefeitoPOSController(
            IDefeitoPOSService DefeitoPOSService
        )
        {
            _DefeitoPOSService = DefeitoPOSService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DefeitoPOSParameters parameters)
        {
            return _DefeitoPOSService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDefeitoPOS}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DefeitoPOS Get(int codDefeitoPOS)
        {
            return _DefeitoPOSService.ObterPorCodigo(codDefeitoPOS);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public DefeitoPOS Post([FromBody] DefeitoPOS d)
        {
            return _DefeitoPOSService.Criar(d);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public DefeitoPOS Put([FromBody] DefeitoPOS d)
        {
            return _DefeitoPOSService.Atualizar(d);
        }

        [HttpDelete("{codDefeitoPOS}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public DefeitoPOS Delete(int codDefeitoPOS)
        {
            return _DefeitoPOSService.Deletar(codDefeitoPOS);
        }
    }
}
