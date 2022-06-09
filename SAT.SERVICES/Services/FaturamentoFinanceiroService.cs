using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace SAT.SERVICES.Services
{
    public class FaturamentoFinanceiroService : IFaturamentoFinanceiroService
    {
        public async Task<FaturamentoFinanceiro> Criar(FaturamentoFinanceiro faturamento)
        {
            var url = "http://perto31.perto.com.br/FaturamentoApi/api/orcamento/CadastraOrcamento";

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(faturamento), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //var receivedReservation = Newtonsoft.Json.JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }

            return null;
        }
    }
}