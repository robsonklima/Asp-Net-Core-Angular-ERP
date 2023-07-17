using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaProtocoloPeriodoTecnicoParameters parameters)
        {
            return _despesaProtocoloService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaProtocoloPeriodoTecnico despesaProtocoloPeriodoTecnico) =>
            _despesaProtocoloService.Criar(despesaProtocoloPeriodoTecnico);
    }
}
