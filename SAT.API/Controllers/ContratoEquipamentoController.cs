using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoEquipamentoController : ControllerBase
    {
        private readonly IContratoEquipamentoService _contratoEquipamentoInterface;

        public ContratoEquipamentoController(IContratoEquipamentoService contratoEquipamentoInterface)
        {
            _contratoEquipamentoInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoEquipamentoParameters parameters)
        {
            return _contratoEquipamentoInterface.ObterPorParametros(parameters);
        }
    }
}
