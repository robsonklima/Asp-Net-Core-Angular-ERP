using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public partial class GeolocalizacaoService : IGeolocalizacaoService
    {
        public async Task<Geolocalizacao> ViacepLocalizationService(GeolocalizacaoParameters parameters)
        {
            HttpClient client = new();

            var response = await client.GetAsync($"https://viacep.com.br/ws/{parameters.EnderecoCEP}/json/");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViacepGeolocation>(conteudo);    

                if (result == null)
                    return null;

                return new Geolocalizacao
                {
                    EnderecoCEP = result?.Logradouro,
                    Latitude = null,
                    Longitude = null,
                    Endereco = result?.Logradouro,
                    Pais = "Brasil",
                    Bairro = result?.Bairro,
                    Cidade = result?.Localidade,
                    Estado = result?.Uf
                };
            }
            return null;
        }
    }
}
