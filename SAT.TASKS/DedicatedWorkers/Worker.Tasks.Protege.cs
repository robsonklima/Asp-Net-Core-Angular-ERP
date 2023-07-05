using System.Net;
using System.Text;
using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async void ExecutarProtegeAsync(SatTask task, List<OrdemServico> chamados)
        {
            await Login();
        }

        private async Task Login()
        {

            _logger.Info($"{ MsgConst.INIC_AUTENTICACAO }");

            try
            {
                var client = new HttpClient();
                
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL);
                client.DefaultRequestHeaders.Add("username", Constants.INTEGRACAO_PROTEGE_USER);
                client.DefaultRequestHeaders.Add("password", Constants.INTEGRACAO_PROTEGE_PASSWORD);
                client.DefaultRequestHeaders.Add("client_id", Constants.INTEGRACAO_PROTEGE_CLIENT_ID);
                client.DefaultRequestHeaders.Add("grant_type", Constants.INTEGRACAO_PROTEGE_GRANT_TYPE);
                client.DefaultRequestHeaders.Add("auth_mode", Constants.INTEGRACAO_PROTEGE_AUTH_MODE);

                StringContent content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, Constants.APPLICATION_JSON);

                using (var response = await client.PostAsync(Constants.INTEGRACAO_PROTEGE_API_URL, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var a = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    }
                    else
                    {
                        _logger.Info($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { apiResponse }");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { ex.Message }");
            }

        }
    }
}