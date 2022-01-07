using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class LaudoController : ControllerBase
    {
        private ILaudoService _laudoService;
        public LaudoController(ILaudoService laudoService)
        {
            _laudoService = laudoService;
        }

        [HttpGet("{codLaudo}")]
        public Laudo ObterPorCodigo(int codLaudo) =>
            _laudoService.ObterPorCodigo(codLaudo);
    }
}