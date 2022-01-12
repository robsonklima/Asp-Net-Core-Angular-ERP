using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System.Threading.Tasks;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class GoogleGeolocationController : ControllerBase
    {
        private readonly IGeolocalizacaoService _geolocalizacaoService;

        public GoogleGeolocationController(IGeolocalizacaoService geolocalizacaoService, ICidadeService cidadeService)
        {
            this._geolocalizacaoService = geolocalizacaoService;
        }

        [HttpGet]
        public async Task<GoogleGeolocation> Get([FromQuery] GoogleGeolocationParameters parameters)
        {
            return await this._geolocalizacaoService.ObterGeolocalizacao(parameters);
        }

        [HttpGet("DistanceMatrix")]
        public async Task<DistanceMatrixResponse> GetDistance([FromQuery] GoogleGeolocationParameters parameters)
        {
            return await this._geolocalizacaoService.GetDistance(parameters);
        }
    }
}
