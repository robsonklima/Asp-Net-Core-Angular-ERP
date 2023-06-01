using NLog.Fluent;
using NLog;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoClienteService : IIntegracaoClienteService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IEquipamentoContratoRepository _equipContratoRepo;

        public IntegracaoClienteService(
            IEquipamentoContratoRepository equipContratoRepo
        )
        {
            _equipContratoRepo = equipContratoRepo;
        }

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            var equipamento = _equipContratoRepo.ObterPorParametros(new EquipamentoContratoParameters {
                NumSerie = data.NumSerie,
                CodClientes = codCliente.ToString(),
                IndAtivo = 1
            });

            OrdemServico os = new OrdemServico
            {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente,
                IndIntegracao = 1,
                CodPosto = 0098,
                CodEquipContrato = 0099
            };

            _logger.Info()
                    .Message(data.ToString())
                    .Property("application", Constants.INTEGRACAO_ZAFFARI)
                    .Write();

            return data;
        }
    }
}