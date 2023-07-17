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
    public class OrcFormaPagamentoController : ControllerBase
    {
        private readonly IOrcFormaPagamentoService _orcFormaPagamentoService;

        public OrcFormaPagamentoController(IOrcFormaPagamentoService orcFormaPagamentoService)
        {
            _orcFormaPagamentoService = orcFormaPagamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OrcFormaPagamentoParameters parameters)
        {
            return _orcFormaPagamentoService.ObterPorParametros(parameters);
        }        
    }
}
