using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        public GoogleGeolocationController(IGeolocalizacaoService geolocalizacaoService)
        {
            this._geolocalizacaoService = geolocalizacaoService;
        }

        private ListViewModel GetGeolocalizacao([FromQuery] GoogleGeolocationParameters parameters)
        {
            return _geolocalizacaoService.ObterPorParametros(parameters);
        }

        private Geolocalizacao PostGeolocalizacao(Geolocalizacao geolocalizacao)
        {
            return _geolocalizacaoService.Criar(geolocalizacao);
        }

        [HttpGet]
        public async Task<GoogleGeolocation> Get([FromQuery] GoogleGeolocationParameters parameters)
        {
            // Verifica se existe no banco a o endereço antes
            ListViewModel localizacoes = this.GetGeolocalizacao(parameters);
            GoogleGeolocation model = new();

            if (((List<Geolocalizacao>)localizacoes.Items).Count > 0)
            {
                string json = ((List<Geolocalizacao>)localizacoes.Items)[0].GoogleGeolocationJson;
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(json);
            }
            else
            {
                var client = new HttpClient();

                var response = await client.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address=" +
                    parameters.EnderecoCEP + "&key=AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var conteudo = await response.Content.ReadAsStringAsync();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(conteudo);

                    if (model.results.Count > 0)
                    {
                        Geolocalizacao novaLocalizacao = new Geolocalizacao()
                        {
                            EnderecoCEP = parameters.EnderecoCEP,
                            Latitude = model.results[0].geometry.location.lat.ToString(),
                            Longitude = model.results[0].geometry.location.lng.ToString(),
                            GoogleGeolocationJson = conteudo
                        };

                        this.PostGeolocalizacao(novaLocalizacao);
                    }
                }
            }

            return model;
        }

        [HttpGet("DistanceMatrix")]
        public async Task<DistanceMatrixResponse> GetDistance([FromQuery] GoogleGeolocationParameters parameters)
        {
            DistanceMatrixResponse model = new DistanceMatrixResponse();

            var response = await new HttpClient().GetAsync("https://maps.googleapis.com/maps/api/distancematrix/json?destinations=" + parameters.LatitudeDestino + "%2C" + parameters.LongitudeDestino + "&origins=" + parameters.LatitudeOrigem + "%2C" + parameters.LongitudeOrigem + "&key=AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrixResponse>(conteudo);
            }

            return model;
        }
    }
}
