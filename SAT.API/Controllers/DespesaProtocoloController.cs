using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaProtocoloController : ControllerBase
    {
        private readonly IDespesaProtocoloService _protocoloService;

        public DespesaProtocoloController(IDespesaProtocoloService protocoloService) =>
            _protocoloService = protocoloService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaProtocoloParameters parameters) =>
            _protocoloService.ObterPorParametros(parameters);

    }
}