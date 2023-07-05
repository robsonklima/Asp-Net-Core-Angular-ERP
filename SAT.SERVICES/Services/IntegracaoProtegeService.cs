using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoProtegeService : IIntegracaoProtegeService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task<ProtegeToken> LoginAsync()
        {
            var jsonToken = new ProtegeToken();
            _logger.Info($"{ MsgConst.INIC_AUTENTICACAO }");

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL + "Token");

                client.DefaultRequestHeaders.Add("username", Constants.INTEGRACAO_PROTEGE_USER);
                client.DefaultRequestHeaders.Add("password", Constants.INTEGRACAO_PROTEGE_PASSWORD);
                client.DefaultRequestHeaders.Add("client_id", Constants.INTEGRACAO_PROTEGE_CLIENT_ID);
                client.DefaultRequestHeaders.Add("grant_type", Constants.INTEGRACAO_PROTEGE_GRANT_TYPE);
                client.DefaultRequestHeaders.Add("auth_mode", Constants.INTEGRACAO_PROTEGE_AUTH_MODE);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    jsonToken = JsonConvert
                        .DeserializeObject<ProtegeToken>(response.Content
                        .ReadAsStringAsync().Result)!;

                    _logger.Info($"{ MsgConst.AUTENTICACAO_OK }");
                }
                else
                {
                    _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { response.StatusCode }");
                }

                return jsonToken;
            }
            catch (Exception ex)
            {
                _logger.Error($"{MsgConst.ERR_API_CLIENTE} {Constants.INTEGRACAO_PROTEGE_API_URL} {ex.Message}");

                return jsonToken;
            }
        }
    
        public async Task<OrdemServicoProtege> ConsultarChamadoAsync(ProtegeToken token, string numOSCliente, string busobId)
        {
            var osCliente = new OrdemServicoProtege();

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL + @$"api/V1/getbusinessobject/busobid/{ busobId }/publicid/{ numOSCliente }");
                client.DefaultRequestHeaders.Add("Content-Type", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Accept", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.Access_token }" );
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    osCliente = JsonConvert
                        .DeserializeObject<dynamic>(response.Content
                        .ReadAsStringAsync().Result)!;

                    _logger.Info($"{ MsgConst.AUTENTICACAO_OK }");

                    return osCliente;
                }
                else
                {
                    _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { response.StatusCode }");

                    return osCliente;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { ex.Message }");

                return osCliente;
            }
        }
    
        public async Task<OrdemServicoProtege> EnviarChamadoAsync(ProtegeToken token)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL + @$"api/V1/savebusinessobject?locale=pt-BR");
                client.DefaultRequestHeaders.Add("Content-Type", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Accept", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.Access_token }" );

                StringContent content = new StringContent(JsonConvert.SerializeObject(new OrdemServicoProtege
                    {
                        
                    }),
                    Encoding.UTF8, Constants.APPLICATION_JSON);     

                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var osCliente = JsonConvert
                        .DeserializeObject<dynamic>(response.Content
                        .ReadAsStringAsync().Result)!;

                    _logger.Info($"{ MsgConst.AUTENTICACAO_OK }");

                    return osCliente;
                }
                else
                {
                    _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { response.StatusCode }");

                    return null!;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { ex.Message }");

                return null!;
            }
        }

        public async Task<OrdemServicoProtegeArmazenados> ConsultarPesquisaArmazenadaAsync(ProtegeToken token)
        {
            try
            {
                var client = new HttpClient();
                var association = "6dd53665c0c24cab86870a21cf6434ae";
                var searchname = "Chamados Perto Abertos";
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL + @$"api/V1/getsearchresults/association/{ association }/scope/global/scopeowner/none/searchname/{ searchname }");
                client.DefaultRequestHeaders.Add("Content-Type", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Accept", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.Access_token }" );

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var armazenados = JsonConvert
                        .DeserializeObject<dynamic>(response.Content
                        .ReadAsStringAsync().Result)!;

                    _logger.Info($"{ MsgConst.AUTENTICACAO_OK }");

                    return armazenados;
                }
                else
                {
                    _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { response.StatusCode }");

                    return null!;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { ex.Message }");

                return null!;
            }
        }

        public async Task<OrdemServicoProtege> AtualizarStatusChamadoAsync(ProtegeToken token)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_PROTEGE_API_URL + @$"api/V1/savebusinessobject?locale=pt-BR");
                client.DefaultRequestHeaders.Add("Content-Type", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Accept", Constants.APPLICATION_JSON );
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.Access_token }" );

                StringContent content = new StringContent(JsonConvert.SerializeObject(new OrdemServicoProtege
                    {
                        
                    }),
                    Encoding.UTF8, Constants.APPLICATION_JSON); 

                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var armazenados = JsonConvert
                        .DeserializeObject<dynamic>(response.Content
                        .ReadAsStringAsync().Result)!;

                    _logger.Info($"{ MsgConst.AUTENTICACAO_OK }");

                    return armazenados;
                }
                else
                {
                    _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { response.StatusCode }");

                    return null!;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ MsgConst.ERR_API_CLIENTE } { Constants.INTEGRACAO_PROTEGE_API_URL } { ex.Message }");

                return null!;
            }
        }
    }
}