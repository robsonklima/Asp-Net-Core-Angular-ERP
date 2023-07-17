using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class DashboardLabController : ControllerBase
    {
        private readonly IDashboardLabService _dashboardLabService;

        public DashboardLabController(IDashboardLabService dashboardLabService)
        {
            _dashboardLabService = dashboardLabService;
        }

        [HttpGet("RecebidosReparados")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterRecebidosReparados(parameters);
        }

        [HttpGet("TopFaltantes")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabTopFaltantes> ObterTopFaltantes([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTopFaltantes(parameters);
        }

        [HttpGet("TempoMedioReparo")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTempoMedioReparo(parameters);
        }

        [HttpGet("ProdutividadeTecnica")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterProdutividadeTecnica(parameters);
        }

        [HttpGet("TopItensMaisAntigos")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabTopItensMaisAntigos> ObterTopItensMaisAntigos([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTopItensMaisAntigos(parameters);
        }

        [HttpGet("IndiceReincidencia")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewDashboardLabIndiceReincidencia> ObterIndiceReincidencia([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterIndiceReincidencia(parameters);
        }
    }
}
