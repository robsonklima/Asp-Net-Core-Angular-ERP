using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class IndicadorController : ControllerBase
    {
        private readonly IIndicadorService _indicadorService;

        public IndicadorController(IIndicadorService indicadorService)
        {
            _indicadorService = indicadorService;
        }

        [HttpGet]
        public List<Indicador> ObterIndicadores([FromQuery] IndicadorParameters parameters)
        {
            return _indicadorService.ObterIndicadores(parameters);
        }
    }
}
