using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class IntegracaoController : ControllerBase
    {
        private readonly IIntegracaoService _integracaoService;

        public IntegracaoController(IIntegracaoService integracaoService)
        {
            _integracaoService = integracaoService;
        }

        [HttpPost("OrdensServico")]
        [AllowAnonymous]
        public Integracao Post([FromBody] Integracao ordem)
        {
            return _integracaoService.Criar(ordem);
        }

        [HttpGet("OrdensServico")]
        [AllowAnonymous]
        public List<Integracao> ObterOrdensServico([FromQuery] IntegracaoParameters parameters)
        {
            return _integracaoService.ConsultarOrdensServico(parameters);
        }

        [HttpGet("Equipamentos")]
        [AllowAnonymous]
        public List<IntegracaoEquipamentoContrato> ObterEquipamentos([FromQuery] IntegracaoParameters parameters)
        {
            return _integracaoService.ConsultarEquipamentos(parameters);
        }
    }
}
