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

        public IntegracaoClienteService(
            ILocalAtendimentoService localService,
            IEquipamentoContratoService equipContratoService
        )
        {
            _localService = localService;
            _equipContratoService = equipContratoService;
        }

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            OrdemServico os = new OrdemServico
            {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente,
                IndIntegracao = 1,
                CodPosto = data.CodigoLocal,
                CodEquipContrato = data.CodigoEquipamento
            };

            _logger.Info()
                    .Message(data.ToString())
                    .Property("application", Constants.INTEGRACAO_ZAFFARI)
                    .Write();

            return data;
        }

        public List<LocalAtendimentoCliente> ObterLocais(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            IEnumerable<LocalAtendimento> locais = (IEnumerable<LocalAtendimento>)_localService.ObterPorParametros(new LocalAtendimentoParameters
            {
                CodCliente = codCliente,
                IndAtivo = 1
            }).Items;

            var targetList = locais
                .Select(loc => new LocalAtendimentoCliente()
                {
                    Codigo = loc.CodPosto.Value,
                    NumAgencia = loc.NumAgencia,
                    DCPosto = loc.DCPosto,
                    Endereco = loc.Endereco,
                    Equipamentos = ObterEquipamentosCliente(loc)
                })
                .ToList();

            throw new System.NotImplementedException();
        }

        private List<EquipamentoCliente> ObterEquipamentosCliente(LocalAtendimento loc)
        {
            var equipamentos = (IEnumerable<EquipamentoContrato>)_equipContratoService.ObterPorParametros(new EquipamentoContratoParameters
            {
                CodPosto = loc.CodPosto,
                CodClientes = loc.CodCliente.ToString(),
                IndAtivo = 1
            }).Items;

            var targetList = equipamentos
                .Select(e => new EquipamentoCliente()
                {
                    Codigo = e.CodEquipContrato,
                    NumSerie = e.NumSerie
                })
                .ToList();

            return null;
        }
    }
}