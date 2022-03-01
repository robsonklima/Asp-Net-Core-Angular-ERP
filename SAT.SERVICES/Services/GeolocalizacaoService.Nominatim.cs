using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public partial class GeolocalizacaoService : IGeolocalizacaoService
    {
        public async Task<Geolocalizacao> NominatimLocalizationService(GeolocalizacaoParameters parameters)
        {
            NominatimGeolocation[] model = { };
            HttpClient client = new();

            var pais = parameters.Pais ?? "Brazil";

            var response = await client.GetAsync
                ($"https://nominatim.openstreetmap.org/search?q={parameters.EnderecoCEP}%{pais}&email={Constants.EQUIPE_SAT_EMAIL}&format=json&addressdetails=1");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<NominatimGeolocation[]>(conteudo);

                var result = model.FirstOrDefault();

                if (result == null)
                    return null;

                return new Geolocalizacao
                {
                    EnderecoCEP = parameters.EnderecoCEP,
                    Cidade = result.address.city,
                    Estado = result.address.state_district,
                    Latitude = result.lat,
                    Longitude = result.lon
                };
            }
            return null;
        }

        public async Task<Geolocalizacao> NominatimRouteService(GeolocalizacaoParameters parameters)
        {
            DistanceMatrix model = new();

            var latDestino = parameters.LatitudeDestino.Replace(',', '.');
            var longDestino = parameters.LongitudeDestino.Replace(',', '.');
            var latOrigem = parameters.LatitudeOrigem.Replace(',', '.');
            var longOrigem = parameters.LongitudeOrigem.Replace(',', '.');

            var response =
            await new HttpClient().GetAsync($"https://www.mapquestapi.com/directions/v2/route?key={Constants.MAP_QUEST_KEY}&useTraffic=true&timeType=1&from={latOrigem},{longOrigem}&to={latDestino},{longDestino}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrix>(conteudo);

                return new Geolocalizacao
                {
                    EnderecoCEP = parameters.EnderecoCEP,
                    EnderecoOrigem = model?.route?.locations?.FirstOrDefault()?.street,
                    CidadeOrigem = model?.route?.locations?.FirstOrDefault()?.adminArea5,
                    EstadoOrigem = model?.route?.locations?.FirstOrDefault()?.adminArea3,
                    Distancia = model.route.distance,
                    Duracao = model.route.time
                };
            }

            return null;
        }
    }
}
