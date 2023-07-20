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

            OrdemServico os = new OrdemServico
            {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente,
                IndIntegracao = 1
            };

            data.NumIncidentePerto = "8765477";

            _logger.Info()
                .Message(@"Incidente do cliente {} aberto com sucesso", Constants.INTEGRACAO_ZAFFARI)
                .Property("application", Constants.INTEGRACAO_ZAFFARI)
                .Write();

            return data;
        }
    }
}