using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoController : ControllerBase
    {
        private readonly IMonitoramentoService _monitoramentoService;

        public MonitoramentoController(IMonitoramentoService monitoramentoService)
        {
            this._monitoramentoService = monitoramentoService;
        }

        [HttpGet]
        public Monitoramento[] Get([FromQuery] MonitoramentoParameters parameters)
        {
            return this._monitoramentoService.ObterPorParametros(parameters);
        }
    }
}