using System;
using System.Collections.Generic;
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
    public class IntegracaoZaffariService : IIntegracaoZaffariService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public IntegracaoZaffariService() { }

        public async Task ExecutarAsync(IEnumerable<OrdemServico> chamados)
        {
            _logger.Info($"{ MsgConst.INI_PROC } { Constants.INTEGRACAO_ZAFFARI }");

            try
            {
                foreach (var chamado in chamados)
                {
                    //await Transmitir(chamado);
                }    
            }
            catch (Exception ex)
            {
                _logger.Error($"{ Constants.INTEGRACAO_ZAFFARI } { ex.Message }");
            }

            _logger.Info(MsgConst.FIN_PROCESSO + Constants.INTEGRACAO_ZAFFARI);
        }


        private async Task<OrdemServicoZaffari> Transmitir(OrdemServico chamado)
        {
            _logger.Info($"{ MsgConst.INIC_TRANSMISSAO }, chamado { chamado.CodOS }");

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_ZAFFARI_API_URL);
                
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                formData.Add(new KeyValuePair<string, string>("username", Constants.INTEGRACAO_ZAFFARI_USER));
                formData.Add(new KeyValuePair<string, string>("password", Constants.INTEGRACAO_ZAFFARI_PASSWORD));
                formData.Add(new KeyValuePair<string, string>("scope", "all"));
                request.Content = new FormUrlEncodedContent(formData);

                StringContent content = new StringContent(JsonConvert.SerializeObject(new OrdemServicoZaffari()),
                    Encoding.UTF8, Constants.APPLICATION_JSON);

                using (var response = await client.PostAsync(Constants.INTEGRACAO_ZAFFARI_API_URL, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<OrdemServicoZaffari>(apiResponse);
                    }
                    else
                    {
                        _logger.Info($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_ZAFFARI_API_URL } { apiResponse }");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_ZAFFARI_API_URL } { ex.Message }");

                return null;
            }
        }
    }
}