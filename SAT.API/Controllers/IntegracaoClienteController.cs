using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class IntegracaoClienteController : ControllerBase
    {
        private readonly IIntegracaoClienteService _integracaoClienteService;

        public IntegracaoClienteController(
            IIntegracaoClienteService integracaoClienteService
        )
        {
            _integracaoClienteService = integracaoClienteService;
        }

        [HttpGet("NovoIncidente")]
        public IntegracaoCliente Integrar([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            return _integracaoClienteService.Integrar(IntegracaoCliente);
        }

        [HttpGet("MeusIncidentes")]
        public List<IntegracaoCliente> GetIncidentes([FromQuery] IntegracaoClienteParameters parameters)
        {
            return _integracaoClienteService.ObterMeusIncidentes(parameters);
        }

        [HttpGet("MeusEquipamentos")]
        public List<EquipamentoCliente> GetEquipamentos([FromQuery] IntegracaoClienteParameters parameters)
        {
            return _integracaoClienteService.ObterMeusEquipamentos(parameters);
        }
    }
}
