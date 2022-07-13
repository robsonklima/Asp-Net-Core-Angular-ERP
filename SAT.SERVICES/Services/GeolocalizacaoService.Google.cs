using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace SAT.SERVICES.Services
{
    public partial class GeolocalizacaoService : IGeolocalizacaoService
    {
        public async Task<Geolocalizacao> GoogleLocalizationService(GeolocalizacaoParameters parameters)
        {
            GoogleGeolocation model = new();
            HttpClient client = new();

            string url = string.Empty;
            if (!string.IsNullOrWhiteSpace(parameters.EnderecoCEP))
                url = $"https://maps.googleapis.com/maps/api/geocode/json?address=CEP-{parameters.EnderecoCEP}-Brazil&key={Constants.GOOGLE_API_KEY}";
            else if (!string.IsNullOrWhiteSpace(parameters.LatitudeOrigem) && !string.IsNullOrWhiteSpace(parameters.LatitudeDestino))
                url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={parameters.LatitudeOrigem.Replace(',', '.')},{parameters.LongitudeOrigem.Replace(',', '.')}&key={Constants.GOOGLE_API_KEY}";
            else 
                throw new Exception("Favor informar o endereço ou as coordenadas");

            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(Constants.ERRO_CONSULTAR_COORDENADAS);

                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(conteudo);

                if (model.results.Count() == 0)
                    throw new Exception(Constants.NENHUM_REGISTRO);

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
                    Pais = pais?.long_name,
                    Bairro = bairro?.long_name,
                    Cidade = cidade?.long_name,
                    Estado = estado?.short_name,
                    Numero = numero?.long_name
                };
            }

            return null;
        }

        public async Task<Geolocalizacao> GoogleRouteService(GeolocalizacaoParameters parameters)
        {
            var latDestino = parameters.LatitudeDestino.Replace(',', '.');
            var longDestino = parameters.LongitudeDestino.Replace(',', '.');
            var latOrigem = parameters.LatitudeOrigem.Replace(',', '.');
            var longOrigem = parameters.LongitudeOrigem.Replace(',', '.');

            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={latOrigem},{longOrigem}&destinations={latDestino},{longDestino}&mode=auto&language=en-EN&key={Constants.GOOGLE_API_KEY}";
            var response =
                await new HttpClient().GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var conteudo = await response.Content.ReadAsStringAsync();
                    GoogleDistanceMatrix model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleDistanceMatrix>(conteudo);
                    Geolocalizacao geo =  new Geolocalizacao
                    {
                        EnderecoOrigem = model.origin_addresses?.First(),
                        EnderecoDestino = model.destination_addresses?.First(),
                        Distancia = model.rows?.First()?.elements?.First()?.distance?.value,
                        Duracao = model.rows?.First()?.elements?.First()?.duration?.value
                    };

                    return geo;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao consultar serviço de API do Google", ex);;
                }
            }

            return null;
        }
    }
}
