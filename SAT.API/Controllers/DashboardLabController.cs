using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

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
        public List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterRecebidosReparados(parameters);
        }

        [HttpGet("TopFaltantes")]
        public List<ViewDashboardLabTopFaltantes> ObterTopFaltantes([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTopFaltantes(parameters);
        }

        [HttpGet("TempoMedioReparo")]
        public List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTempoMedioReparo(parameters);
        }

        [HttpGet("ProdutividadeTecnica")]
        public List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterProdutividadeTecnica(parameters);
        }

        [HttpGet("TopItensMaisAntigos")]
        public List<ViewDashboardLabTopItensMaisAntigos> ObterTopItensMaisAntigos([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterTopItensMaisAntigos(parameters);
        }

        [HttpGet("IndiceReincidencia")]
        public List<ViewDashboardLabIndiceReincidencia> ObterIndiceReincidencia([FromQuery] DashboardLabParameters parameters)
        {
            return _dashboardLabService.ObterIndiceReincidencia(parameters);
        }
    }
}
