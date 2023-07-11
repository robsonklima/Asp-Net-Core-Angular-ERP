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

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] LaudoParameters parameters)
        {
            return _laudoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codLaudo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Laudo ObterPorCodigo(int codLaudo) =>
            _laudoService.ObterPorCodigo(codLaudo);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Laudo laudo)
        {
            _laudoService.Atualizar(laudo);
        }

    }
}