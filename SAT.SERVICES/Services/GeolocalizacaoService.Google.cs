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
        public async Task<Geolocalizacao> GoogleLocalizationService(GeolocalizacaoParameters parameters)
        {
            GoogleGeolocation model = new();
            HttpClient client = new();

            var response = await client.GetAsync
                    (string.IsNullOrWhiteSpace(parameters.EnderecoCEP) ?
                    $"https://maps.googleapis.com/maps/api/geocode/json?latlng={parameters.LatitudeOrigem.Replace(',', '.')},{parameters.LongitudeOrigem.Replace(',', '.')}&key={Constants.GOOGLE_API_KEY}" :
                    $"https://maps.googleapis.com/maps/api/geocode/json?address=CEP-{parameters.EnderecoCEP}-Brazil&key={Constants.GOOGLE_API_KEY}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(conteudo);

                var bairro = model.results[0].address_components.Where(ac => ac.types.Contains("sublocality")).FirstOrDefault();
                var cidade = model.results[0].address_components.Where(ac => ac.types.Contains("administrative_area_level_2")).FirstOrDefault();
                var estado = model.results[0].address_components.Where(ac => ac.types.Contains("administrative_area_level_1")).FirstOrDefault();
                var endereco = model.results[0].formatted_address;
                var numero = model.results[0].address_components.Where(ac => ac.types.Contains("street_number")).FirstOrDefault();
                var pais = model.results[0].address_components.Where(ac => ac.types.Contains("country")).FirstOrDefault();

                return new Geolocalizacao
                {
                    EnderecoCEP = parameters?.EnderecoCEP,
                    Latitude = model?.results[0]?.geometry.location?.lat.ToString(),
                    Longitude = model?.results[0]?.geometry.location?.lng.ToString(),
                    Endereco = endereco,
                    Pais = pais.long_name,
                    Bairro = bairro?.long_name,
                    Cidade = cidade?.long_name,
                    Estado = estado?.short_name
                };
            }

            return null;
        }
    }
}
