using NLog.Fluent;
using NLog;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class IntegracaoClienteService : IIntegracaoClienteService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            OrdemServico os = new OrdemServico {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente
            };

            _logger.Info()
                    .Message(data.ToString())
                    .Property("application", Constants.INTEGRACAO_ZAFFARI)
                    .Write();

            return data;
        }
    }
}