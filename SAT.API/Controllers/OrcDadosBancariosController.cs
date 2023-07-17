using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrcDadosBancariosController : ControllerBase
    {
        private readonly IOrcDadosBancariosService _orcDadosBancariosService;

        public OrcDadosBancariosController(IOrcDadosBancariosService orcDadosBancariosService)
        {
            _orcDadosBancariosService = orcDadosBancariosService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcDadosBancariosParameters parameters)
        {
            return _orcDadosBancariosService.ObterPorParametros(parameters);
        }        
    }
}
