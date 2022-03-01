using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ContratoEquipamentoDataController : ControllerBase
    {
        private readonly IContratoEquipamentoDataService _contratoEquipamentoDataInterface;

        public ContratoEquipamentoDataController(IContratoEquipamentoDataService contratoEquipamentoInterface)
        {
            _contratoEquipamentoDataInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoEquipamentoDataParameters parameters)
        {
            return _contratoEquipamentoDataInterface.ObterPorParametros(parameters);
        }
    }
}
