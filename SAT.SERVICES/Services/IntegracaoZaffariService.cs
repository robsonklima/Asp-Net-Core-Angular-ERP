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
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoZaffariService : IIntegracaoZaffariService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ISatTaskProcessoService _taskProcessoService;
        private IOrdemServicoService _osService;

        public IntegracaoZaffariService(
            ISatTaskProcessoService taskProcessoService,
            IOrdemServicoService osService
        )
        {
            _taskProcessoService = taskProcessoService;
            _osService = osService;
        }

        public async Task ExecutarAsync()
        {
            _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_ZAFFARI);

            var processos = ObterFilaProcessos();
            ProcessarFilaProcessos(processos);
            var chamado = new OrdemServico { };
            await Transmitir(chamado);

            _logger.Info(MsgConst.FIN_PROCESSO + Constants.INTEGRACAO_ZAFFARI);
        }

        private IEnumerable<SatTaskProcesso> ObterFilaProcessos()
        {
            _logger.Info(MsgConst.OBTENDO_PROCESSOS + Constants.INTEGRACAO_ZAFFARI);

            var processos = (IEnumerable<SatTaskProcesso>)_taskProcessoService
                .ObterPorParametros(new SatTaskProcessoParameters
                {
                    CodSatTaskTipo = (int)SatTaskTipoEnum.INT_ZAFFARI,
                    Status = SatTaskStatusConst.PENDENTE,
                })
                .Items;

            _logger.Info(MsgConst.QTD_PROCESSOS + processos.Count());

            return processos;
        }

        private async void ProcessarFilaProcessos(IEnumerable<SatTaskProcesso> processos)
        {
            _logger.Info(MsgConst.PROC_PROCESSOS);

            foreach (var processo in processos)
            {
                var chamadoPerto = _osService.ObterPorCodigo(processo.CodOS);

                _logger.Info(MsgConst.OS_PERTO + chamadoPerto.CodOS);

                await Transmitir(chamadoPerto);

                AtualizarProcesso(processo);
            }
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

        private void AtualizarProcesso(SatTaskProcesso processo)
        {
            _logger.Info(MsgConst.CRIANDO_PROCESSO);

            processo.DataHoraProcessamento = DateTime.Now;
            processo.Status = SatTaskStatusConst.PROCESSADO;
            _taskProcessoService.Atualizar(processo);

            _logger.Info(MsgConst.PROCESSO_CRIADO);
        }
    }
}