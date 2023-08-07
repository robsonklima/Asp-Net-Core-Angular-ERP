using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

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

        [HttpPost("NovoIncidente")]
        //[ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public IntegracaoCliente Integrar([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            return _integracaoClienteService.Integrar(IntegracaoCliente);
        }

        [HttpPut("AtualizarIncidente")]
        //[ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public IntegracaoCliente Put([FromBody] IntegracaoCliente integracaoCliente)
        {
            return _integracaoClienteService.Atualizar(integracaoCliente);
        }
    }
}
