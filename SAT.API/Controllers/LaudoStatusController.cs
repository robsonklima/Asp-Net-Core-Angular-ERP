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
    public class LaudoStatusController : ControllerBase
    {
        private ILaudoStatusService _laudoStatusService;
        public LaudoStatusController(ILaudoStatusService laudoStatusService)
        {
            _laudoStatusService = laudoStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] LaudoStatusParameters parameters)
        {
            return _laudoStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codLaudoStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public LaudoStatus ObterPorCodigo(int codLaudoStatus) =>
            _laudoStatusService.ObterPorCodigo(codLaudoStatus);

    }
}