using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

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
        public MonitoramentoViewModel Get()
        {
            return this._monitoramentoService.ObterListaMonitoramento();
        }
    }
}