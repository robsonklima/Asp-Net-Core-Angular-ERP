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
    public class TipoFreteController : ControllerBase
    {
        private readonly ITipoFreteService _tipoFreteService;

        public TipoFreteController(ITipoFreteService tipoFreteService)
        {
            _tipoFreteService = tipoFreteService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoFreteParameters parameters)
        {
            return _tipoFreteService.ObterPorParametros(parameters);
        }
    }
}
