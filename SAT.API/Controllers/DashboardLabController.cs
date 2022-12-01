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
    }
}
