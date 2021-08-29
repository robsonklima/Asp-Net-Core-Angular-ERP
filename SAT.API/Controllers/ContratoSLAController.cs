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
    public class ContratoSLAController : ControllerBase
    {
        public IContratoSLAService _contratoSLAService { get; }

        public ContratoSLAController(IContratoSLAService contratoSLAService)
        {
            _contratoSLAService = contratoSLAService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoSLAParameters parameters)
        {
            return _contratoSLAService.ObterPorParametros(parameters);
        }
    }
}
