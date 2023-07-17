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
    public class ProtocoloSTNController : ControllerBase
    {
        private readonly IProtocoloSTNService _protocoloSTNService;

        public ProtocoloSTNController(IProtocoloSTNService protocoloSTNService)
        {
            _protocoloSTNService = protocoloSTNService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ProtocoloSTNParameters parameters)
        {
            return _protocoloSTNService.ObterPorParametros(parameters);
        }

        [HttpGet("{codProtocoloSTN}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ProtocoloSTN Get(int codProtocoloSTN)
        {
            return _protocoloSTNService.ObterPorCodigo(codProtocoloSTN);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public ProtocoloSTN Post([FromBody] ProtocoloSTN protocolo)
        {
            return _protocoloSTNService.Criar(protocolo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ProtocoloSTN protocolo)
        {
            _protocoloSTNService.Atualizar(protocolo);
        }

        [HttpDelete("{codProtocoloSTN}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codProtocoloSTN)
        {
            _protocoloSTNService.Deletar(codProtocoloSTN);
        }
    }
}