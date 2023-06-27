using System;
using System.Collections.Generic;
using System.Linq;
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
        private IOrdemServicoService _osService;

        public IntegracaoZaffariService(
            IOrdemServicoService osService
        )
        {
            _osService = osService;
        }

        public async Task ExecutarAsync(IEnumerable<OrdemServico> chamados)
        {
            _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_ZAFFARI);

            foreach (var chamado in chamados)
            {
                await Transmitir(chamado);
            }

            _logger.Info(MsgConst.FIN_PROCESSO + Constants.INTEGRACAO_ZAFFARI);
        }


        private async Task<OrdemServicoZaffari> Transmitir(OrdemServico chamado)
        {
            _logger.Info(MsgConst.INIC_TRANSMISSAO);

            var ordem = new OrdemServicoZaffari();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(ordem), Encoding.UTF8, Constants.APPLICATION_JSON);

                try
                {
                    using (var response = await httpClient.PostAsync(Constants.INTEGRACAO_ZAFFARI_API_URL, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            _logger.Info(MsgConst.FIN_TRANSMISSAO);

                            return JsonConvert.DeserializeObject<OrdemServicoZaffari>(apiResponse);
                        }
                        else
                        {
                            _logger.Error(MsgConst.ERR_API_CLIENTE + Constants.INTEGRACAO_ZAFFARI_API_URL + " " + apiResponse);

                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Info(MsgConst.ERR_API_CLIENTE + Constants.INTEGRACAO_ZAFFARI_API_URL + ex.Message);

                    return null;
                }
            }
        }
    }
}