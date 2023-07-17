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
    public class DespesaItemAlertaController : ControllerBase
    {
        private readonly IDespesaItemAlertaService _alertaService;

        public DespesaItemAlertaController(IDespesaItemAlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaItemAlertaParameters parameters)
        {
            return _alertaService.ObterPorParametros(parameters);
        }
    }
}