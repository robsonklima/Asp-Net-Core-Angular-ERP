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
            var chamado = new OrdemServico {};

            await Transmitir(chamado);
        }

        private async Task<OrdemServicoZaffari> Transmitir(OrdemServico chamado)
        {
            var ordem = new OrdemServicoZaffari();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(ordem), Encoding.UTF8, Constants.APPLICATION_JSON);

                using (var response = await httpClient.PostAsync(Constants.INTEGRACAO_ZAFFARI_API_URL, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<OrdemServicoZaffari>(apiResponse);
                }
            }
        }
    }
}