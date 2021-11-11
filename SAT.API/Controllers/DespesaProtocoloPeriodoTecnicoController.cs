using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaProtocoloPeriodoTecnicoController : ControllerBase
    {
        private readonly IDespesaProtocoloPeriodoTecnicoService _despesaProtocoloService;

        public DespesaProtocoloPeriodoTecnicoController(IDespesaProtocoloPeriodoTecnicoService despesaProtocoloService)
        {
            _despesaProtocoloService = despesaProtocoloService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaProtocoloPeriodoTecnicoParameters parameters)
        {
            return _despesaProtocoloService.ObterPorParametros(parameters);
        }
    }
}
