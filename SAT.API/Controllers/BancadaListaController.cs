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
    public class BancadaListaController : ControllerBase
    {
        private readonly IBancadaListaService _clienteBancadaService;

        public BancadaListaController(IBancadaListaService bancadaListaService)
        {
            _clienteBancadaService = bancadaListaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] BancadaListaParameters parameters)
        {
            return _clienteBancadaService.ObterPorParametros(parameters);
        }
    }
}
