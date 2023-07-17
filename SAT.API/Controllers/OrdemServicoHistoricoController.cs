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
    public class OrdemServicoHistoricoController : ControllerBase
    {
        private IOrdemServicoHistoricoService _osHistoricoService;
        public OrdemServicoHistoricoController(IOrdemServicoHistoricoService osHistoricoService)
        {
            _osHistoricoService = osHistoricoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel ObterPorParametros([FromQuery] OrdemServicoHistoricoParameters parameters)
        {
            return _osHistoricoService.ObterPorParametros(parameters);
        }
    }
}
