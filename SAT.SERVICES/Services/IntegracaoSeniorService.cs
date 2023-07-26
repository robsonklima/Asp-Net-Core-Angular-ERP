using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoSeniorService : IIntegracaoSeniorService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task<IntegracaoSenior> ExecutarAsync()
        {
            _logger.Info($"{MsgConst.INIC_TRANSMISSAO} {Constants.INTEGRACAO_SENIOR}");

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_SENIOR_API_URL);

                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                var encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                byte[] headerBytes = encoding.GetBytes(Constants.INTEGRACAO_SENIOR_USER + ":" + Constants.INTEGRACAO_SENIOR_PASSWORD);
                string headerValue = Convert.ToBase64String(headerBytes);
                request.Headers.Add("Authorization", "Basic " + headerValue);

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new IntegracaoSenior { }),
                    Encoding.UTF8, Constants.APPLICATION_JSON);

                using (var response = await client.PostAsync(Constants.INTEGRACAO_SENIOR_API_URL, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    }
                    else
                    {
                        _logger.Info($"{MsgConst.ERR_API_CLIENTE} {Constants.INTEGRACAO_SENIOR_API_URL} {apiResponse}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{MsgConst.ERR_API_CLIENTE} {Constants.INTEGRACAO_SENIOR_API_URL} {ex.Message}");

                return null;
            }
        }
    }
}