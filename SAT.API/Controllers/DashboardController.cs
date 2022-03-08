using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.ViewModels.Dashboard;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public ViewDadosDashboard ObterViewPorParametros([FromQuery] ViewDadosDashboardParameters viewDadosDashboardParameters)
        {
            return _dashboardService.ObterViewPorParametros(viewDadosDashboardParameters);
        }
    }
}
