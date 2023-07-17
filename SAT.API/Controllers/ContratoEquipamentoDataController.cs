using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ContratoEquipamentoDataParameters parameters)
        {
            return _contratoEquipamentoDataInterface.ObterPorParametros(parameters);
        }
    }
}
