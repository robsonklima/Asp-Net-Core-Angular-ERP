using NLog.Fluent;
using NLog;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using System;

namespace SAT.SERVICES.Services
{
    public class IntegracaoClienteService : IIntegracaoClienteService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ILocalAtendimentoService _localService;
        private IEquipamentoContratoService _equipContratoService;
        private IOrdemServicoService _osService;

        public IntegracaoClienteService(
            ILocalAtendimentoService localService,
            IEquipamentoContratoService equipContratoService,
            IOrdemServicoService osService
        )
        {
            _localService = localService;
            _equipContratoService = equipContratoService;
            _osService = osService;
        }

        public IntegracaoCliente Atualizar(IntegracaoCliente data)
        {
            throw new Exception("Not Implemented");
        }

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

                 var equipamento = _equipContratoService.ObterPorParametros(new EquipamentoContratoParameters
                {
                    NumSerie = data.NumSerie,
                    CodClientes = codCliente.ToString(),
                    IndAtivo = 1
                });

                var agenciaDc = data.NumAgencia.Split("/");

                    string agencia = agenciaDc[0];
                    string dc = agenciaDc[1];

                var local = _localService.ObterPorParametros(new LocalAtendimentoParameters
                {
                    NumAgencia = agencia,
                    DCPosto = dc,
                    CodClientes = codCliente.ToString(),
                    IndAtivo = 1
                });

            OrdemServico os = new OrdemServico
            {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                ObservacaoCliente = data.Observacao,
                CodCliente = codCliente,
				CodStatusServico = 1,
                CodTipoIntervencao = 2,
				CodEquipContrato = equipamento.CodEquipContrato,
                CodEquip = equipamento.CodEquip,
                CodTipoEquip = equipamento.CodTipoEquip,
                CodGrupoEquip = equipamento.CodGrupoEquip,
                CodFilial = equipamento.CodFilial,
                CodAutorizada = equipamento.CodAutorizada,
                CodRegiao = equipamento.CodRegiao,
                CodPosto = local.CodPosto,
                IndIntegracao = 1,
                IndServico = 1,
                DataHoraSolicitacao = DateTime.Now,
				DataHoraCad = DateTime.Now,
				DataHoraAberturaOS = DateTime.Now,
				CodUsuarioCad = "INTEGRACAO_ZAFFARI",
				IndStatusEnvioReincidencia = -1,
				IndRevisaoReincidencia = 1,
				IndRevOK = null
            };

            OrdemServico retornoOs = _osService.Criar(os);

            data.NumIncidentePerto = retornoOs.CodOS.ToString();
            data.DataHoraAberturaPerto =  DateTime.Now;

            _logger.Info()
                .Message(@"Incidente do cliente {} aberto com sucesso", Constants.INTEGRACAO_ZAFFARI)
                .Property("application", Constants.INTEGRACAO_ZAFFARI)
                .Write();

            return data;
        }
    }
}