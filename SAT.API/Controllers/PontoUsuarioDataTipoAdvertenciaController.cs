using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoUsuarioDataTipoAdvertenciaController : ControllerBase
    {
        private readonly IPontoUsuarioDataTipoAdvertenciaService _pontoUsuarioDataTipoAdvertenciaService;

        public PontoUsuarioDataTipoAdvertenciaController(IPontoUsuarioDataTipoAdvertenciaService pontoUsuarioDataTipoAdvertenciaService)
        {
            _pontoUsuarioDataTipoAdvertenciaService = pontoUsuarioDataTipoAdvertenciaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoUsuarioDataTipoAdvertenciaParameters parameters)
        {
            return _pontoUsuarioDataTipoAdvertenciaService.ObterPorParametros(parameters);
        }
    }
}
