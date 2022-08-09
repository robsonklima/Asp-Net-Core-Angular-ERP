using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Services
{
    public class IntegracaoFinanceiroService : IIntegracaoFinanceiroService
    {
        private IIntegracaoFinanceiroRepository _integracaoFinanceiroRepo;

        public IntegracaoFinanceiroService(
            IIntegracaoFinanceiroRepository integracaoFinanceiroRepo
        ) {
            _integracaoFinanceiroRepo = integracaoFinanceiroRepo;
        }

        public async void ExecutarAsync()
        {
            var tiposFaturamento = Enum.GetValues(typeof(TipoFaturamentoOrcEnum));

            foreach (var tipo in tiposFaturamento)  
            {
                var orcamentos = _integracaoFinanceiroRepo
                    .ObterOrcamentos(new IntegracaoFinanceiroParameters { 
                        CodStatusServico = (int)StatusServicoEnum.FECHADO,
                        CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO,
                        TipoFaturamento = (TipoFaturamentoOrcEnum)tipo,
                        DataFechamento = DateTime.Now
                    })
                    .ToList();

                for (int i = 0; i < orcamentos.Count; i++)
                {
                    orcamentos[i].Itens = _integracaoFinanceiroRepo
                        .ObterOrcamentoItens(new IntegracaoFinanceiroParameters { 
                            CodOrc = orcamentos[i].CodOrc, 
                            TipoFaturamento = (TipoFaturamentoOrcEnum)tipo
                        })
                        .ToList();

                    await EnviarDadosAsync(orcamentos[i]);
                }
            }  
        }

        private async Task EnviarDadosAsync(ViewIntegracaoFinanceiroOrcamento orcamento)
        {
            try
            {
                var token = await ObterTokenAsync();
                HttpClient client = new();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonString = JsonConvert.SerializeObject(orcamento);
                HttpResponseMessage response = await client
                    .PostAsync(Constants.INTEGRACAO_FINANCEIRO_API_URL + "/FaturamentoApi/api/orcamento/CadastraOrcamento", 
                        new StringContent(jsonString, Encoding.UTF8, "application/json")); 
                var retorno = JsonConvert.DeserializeObject<IntegracaoFinanceiroRetorno>(response.Content.ReadAsStringAsync()?.Result);

                if (retorno.Sucesso) {
                    LoggerService.LogInfo($"Integração Financeiro: Orçamento {orcamento.CodOrc} integrado com sucesso!");
                } else {
                    LoggerService.LogError($"Integração Financeiro: Orçamento {orcamento.CodOrc} integrado com erro! {retorno.MensagemErro}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar orçamento Integração Financeiro" + ex.Message);
            }
        }

        private async Task<string> ObterTokenAsync() {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.INTEGRACAO_FINANCEIRO_API_URL + "/FaturamentoApi/token");
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.INTEGRACAO_FINANCEIRO_API_URL + "/FaturamentoApi/token");

                var byteArray = new UTF8Encoding().GetBytes("<clientid>:<clientsecret>");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                formData.Add(new KeyValuePair<string, string>("username", Constants.INTEGRACAO_FINANCEIRO_USER));
                formData.Add(new KeyValuePair<string, string>("password", Constants.INTEGRACAO_FINANCEIRO_PASSWORD));
                formData.Add(new KeyValuePair<string, string>("scope", "all"));

                request.Content = new FormUrlEncodedContent(formData);
                var response = await client.SendAsync(request);
                var auth = JsonConvert.DeserializeObject<IntegracaoFinanceiroAutenticacao>(response.Content.ReadAsStringAsync()?.Result);

                return auth?.Access_token;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar token Integração Financeiro" + ex.Message);
            }
        }
    }
}
