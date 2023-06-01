using NLog.Fluent;
using NLog;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using System.Collections.Generic;
using SAT.MODELS.Entities.Params;

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

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            OrdemServico os = new OrdemServico
            {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente,
                IndIntegracao = 1
            };

            _logger.Info()
                    .Message(data.ToString())
                    .Property("application", Constants.INTEGRACAO_ZAFFARI)
                    .Write();

            return data;
        }

        public List<EquipamentoCliente> ObterMeusEquipamentos(IntegracaoClienteParameters par)
        {
            throw new System.NotImplementedException();
        }

        public List<IntegracaoCliente> ObterMeusIncidentes(IntegracaoClienteParameters par)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(par.Chave);

            var osParams = new OrdemServicoParameters { CodCliente = codCliente };

            var oss = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(osParams).Items;

            var incidentes = oss.Select(os => new IntegracaoCliente { 
                NumIncidentePerto = os.CodOS.ToString(),
                NumIncidenteCliente = os.NumOSCliente,
                RelatoCliente = os.DefeitoRelatado,
                NumSerie = os.EquipamentoContrato?.NumSerie
            }).ToList();

            return incidentes;
        }
    }
}