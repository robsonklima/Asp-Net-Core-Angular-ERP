using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class OrcamentoStatusController : ControllerBase
    {
        private IOrcamentoStatusService _orcStatusService;
        public OrcamentoStatusController(IOrcamentoStatusService orcStatusService)
        {
            _orcStatusService = orcStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoStatusParameters parameters) =>
            _orcStatusService.ObterPorParametros(parameters);
    }
}