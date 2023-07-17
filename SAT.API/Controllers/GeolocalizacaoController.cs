using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class GeolocalizacaoController : ControllerBase
    {
        private readonly IGeolocalizacaoService _geolocalizacaoService;

        public GeolocalizacaoController(IGeolocalizacaoService geolocalizacaoService)
        {
            this._geolocalizacaoService = geolocalizacaoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public async Task<Geolocalizacao> Get([FromQuery] GeolocalizacaoParameters parameters)
        {
            return await this._geolocalizacaoService.ObterGeolocalizacao(parameters);
        }

        [HttpGet("DistanceMatrix")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public async Task<Geolocalizacao> GetRoute([FromQuery] GeolocalizacaoParameters parameters)
        {
            return await this._geolocalizacaoService.BuscarRota(parameters);
        }
    }
}