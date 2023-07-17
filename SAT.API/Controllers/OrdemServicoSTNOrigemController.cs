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
    public class OrdemServicoSTNOrigemController : ControllerBase
    {
        private readonly IOrdemServicoSTNOrigemService _OrdemServicoSTNOrigemService;

        public OrdemServicoSTNOrigemController(IOrdemServicoSTNOrigemService OrdemServicoSTNOrigemService)
        {
            _OrdemServicoSTNOrigemService = OrdemServicoSTNOrigemService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrdemServicoSTNOrigemParameters parameters)
        {
            return _OrdemServicoSTNOrigemService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOrigemChamadoSTN}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OrdemServicoSTNOrigem Get(int codOrigemChamadoSTN)
        {
            return _OrdemServicoSTNOrigemService.ObterPorCodigo(codOrigemChamadoSTN);
        }
    }
}
