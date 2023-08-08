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
                    if(chamado.CodCliente == ClienteEnum.ZAFFARI)
                        await Transmitir(chamado);
                }    
            }
            catch (Exception ex)
            {
                _logger.Error($"{ Constants.INTEGRACAO_ZAFFARI } { ex.Message }");
            }

            _logger.Info($"{ MsgConst.FIN_PROC } { Constants.INTEGRACAO_ZAFFARI }");
        }


        private async Task<OrdemServicoZaffari> Transmitir(OrdemServico chamado)
        {
            _logger.Info($"{ MsgConst.INIC_TRANSMISSAO }, chamado { chamado.CodOS }");

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_ZAFFARI_API_URL);
                
                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                var encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                byte[] headerBytes = encoding.GetBytes(Constants.INTEGRACAO_ZAFFARI_USER + ":" + Constants.INTEGRACAO_ZAFFARI_PASSWORD);
                string headerValue = Convert.ToBase64String(headerBytes);
                request.Headers.Add("Authorization", "Basic " + headerValue);

                StringContent content = new StringContent(JsonConvert.SerializeObject(new OrdemServicoZaffari() 
                    {
                        Number = chamado.CodOS,
                        Opened_at = " ",
                        Opened_by = " ",
                        Caller_id = " ",
                        Location = " ",
                        Priority = " ",
                        Impact = " ",
                        Urgency = " ",
                        Category = " ",
                        Subcategory = " ",
                        U_item = " ",
                        State = " ",
                        Short_description = " ",
                        Description = " ",
                        Comments = "TESTE",
                    }),
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