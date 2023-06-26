using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoZaffariService : IIntegracaoZaffariService
    {
        public async Task ExecutarAsync()
        {
            await Transmitir();
        }

        private async Task<OrdemServicoZaffari> Transmitir()
        {
            var ordem = new OrdemServicoZaffari();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(ordem), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Constants.INTEGRACAO_ZAFFARI_API_URL, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<OrdemServicoZaffari>(apiResponse);
                }
            }
        }
    }
}