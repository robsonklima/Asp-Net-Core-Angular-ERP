using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoHistoricoController : ControllerBase
    {
        private readonly IMonitoramentoHistoricoService _monitoramentoHistoricoService;

        public MonitoramentoHistoricoController(IMonitoramentoHistoricoService MonitoramentoHistoricoService)
        {
            this._monitoramentoHistoricoService = MonitoramentoHistoricoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MonitoramentoParameters parameters)
        {
            return this._monitoramentoHistoricoService.ObterPorParametros(parameters);
        }
    }
}