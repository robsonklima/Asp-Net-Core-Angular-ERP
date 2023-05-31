using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
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

        [HttpPost("Abertura")]
        public IntegracaoCliente Integrar([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            return _integracaoClienteService.Integrar(IntegracaoCliente);
        }

        [HttpPost("Locais")]
        public List<LocalAtendimentoCliente> ObterLocais([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            return _integracaoClienteService.ObterLocais(IntegracaoCliente);
        }
    }
}
