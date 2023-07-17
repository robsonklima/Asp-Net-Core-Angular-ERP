using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ViewDadosDashboard ObterViewPorParametros([FromQuery] DashboardParameters parameters)
        {
            return _dashboardService.ObterViewPorParametros(parameters);
        }
    }
}
