using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SAT.MODELS.Entities;
using NLog;

namespace SAT.SERVICES.Services
{
    public class IntegracaoFinanceiroService : IIntegracaoFinanceiroService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IIntegracaoFinanceiroRepository _integracaoFinanceiroRepo;
        private IOrcamentoRepository _orcamentoRepo;
        private string _token;

        public IntegracaoFinanceiroService(
            IIntegracaoFinanceiroRepository integracaoFinanceiroRepo,
            IOrcamentoRepository orcamentoRepo
        ) {
            _integracaoFinanceiroRepo = integracaoFinanceiroRepo;
            _orcamentoRepo = orcamentoRepo;
        }

        public async void ExecutarAsync()
        {
            _token = await ObterTokenAsync();
            //var orcamentoFinanceiro = ObterDadosAsync("FRS76611");
            await EnviarOrcamentosAsync();
        }

        private async Task EnviarOrcamentosAsync() {    

            var orcamentos = _integracaoFinanceiroRepo
                .ObterOrcamentos(new IntegracaoFinanceiroParameters { 
                    //CodStatusServico = (int)StatusServicoEnum.FECHADO,
                    //CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO,
                    AnoFechamento = DateTime.Now,
                    CodOrc = 76611
                })
                .ToList();

            foreach (var orcamento in orcamentos)
            {               
                if (orcamento.TipoFaturamento == (int)TipoFaturamentoOrcEnum.SERVICO)
                {
                    orcamento.Itens = _integracaoFinanceiroRepo
                        .ObterOrcamentoItens(new IntegracaoFinanceiroParameters { 
                            CodOrc = orcamento.CodOrc, 
                            TipoFaturamento = TipoFaturamentoOrcEnum.SERVICO
                        })
                        .ToList();

                    await EnviarOrcamentoAsync(orcamento);
                }

                if (orcamento.TipoFaturamento == (int)TipoFaturamentoOrcEnum.PRODUTO)
                {
                    orcamento.Itens = _integracaoFinanceiroRepo
                        .ObterOrcamentoItens(new IntegracaoFinanceiroParameters { 
                            CodOrc = orcamento.CodOrc, 
                            TipoFaturamento = TipoFaturamentoOrcEnum.PRODUTO
                        })
                        .ToList();

                    await EnviarOrcamentoAsync(orcamento);
                }   
            } 
        }

        private async Task EnviarOrcamentoAsync(ViewIntegracaoFinanceiroOrcamento orcamento)
        {
            try
            {
                HttpClient client = new();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var jsonString = JsonConvert.SerializeObject(orcamento);
                HttpResponseMessage response = await client
                    .PostAsync(Constants.INTEGRACAO_FINANCEIRO_API_URL + "/FaturamentoApi/api/orcamento/CadastraOrcamento", 
                        new StringContent(jsonString, Encoding.UTF8, "application/json")); 
                var retorno = JsonConvert.DeserializeObject<IntegracaoFinanceiroRetorno>(response.Content.ReadAsStringAsync()?.Result);

                if (retorno.Sucesso) {
                    _integracaoFinanceiroRepo.SalvarRetorno(new OrcIntegracaoFinanceiro {
                        CodOrc = orcamento.CodOrc,
                        TipoFaturamento = orcamento.TipoFaturamento,
                        DataHoraCad = DateTime.Now
                    });

                    _logger.Info($@"Integração Financeiro: Orçamento {orcamento.CodOrc} integrado com sucesso!");
                } else {
                    _logger.Info($@"Integração Financeiro: Orçamento {orcamento.CodOrc} integrado com erro!
                        {retorno.MensagemErro} {JsonConvert.SerializeObject(orcamento)}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar orçamento Integração Financeiro" + ex.Message);
            }
        }

        private async Task<IntegracaoFinanceiroOrcamento> ObterDadosAsync(string nroOrcamento) {
            try
            {
                HttpClient client = new();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                HttpResponseMessage response = await client
                    .GetAsync(Constants.INTEGRACAO_FINANCEIRO_API_URL + "/FaturamentoApi/api/orcamento/ConsultarOrcamento?NumeroOrcamento=" + nroOrcamento); 
                return JsonConvert.DeserializeObject<IntegracaoFinanceiroOrcamento>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados da integração financeiro" + ex.Message);
            }
        }

        private void ObterOrcamentosPendentesFinanceiro() {
            var orcamentos = _orcamentoRepo.ObterPorParametros(new OrcamentoParameters {
                
            });
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
