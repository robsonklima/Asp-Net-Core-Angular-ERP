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
    public class MoedaController : ControllerBase
    {
        private readonly IMoedaService _moedaService;

        public MoedaController(IMoedaService moedaPagamentoService)
        {
            _moedaService = moedaPagamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] MoedaParameters parameters)
        {
            return _moedaService.ObterPorParametros(parameters);
        }
    }
}
