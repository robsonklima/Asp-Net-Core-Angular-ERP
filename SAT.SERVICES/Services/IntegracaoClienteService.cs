using NLog.Fluent;
using NLog;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using System.Linq;
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

        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            var equipamento = _equipContratoService.ObterPorParametros(new EquipamentoContratoParameters {
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

            _logger.Info()
                    .Message(data.ToString())
                    .Property("application", Constants.INTEGRACAO_ZAFFARI)
                    .Write();

            return data;
        }

        public List<EquipamentoCliente> ObterMeusEquipamentos(IntegracaoClienteParameters par)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(par.Chave);

            var equipParams = new EquipamentoContratoParameters { 
                CodClientes = codCliente.ToString(),
                IndAtivo = 1
            };

            var equips = (IEnumerable<EquipamentoContrato>)_equipContratoService.ObterPorParametros(equipParams).Items;

            var equipamentos = equips.Select(e => new EquipamentoCliente { 
                Codigo = e.CodEquipContrato,
                NumSerie = e.NumSerie,
                Local = new LocalAtendimentoCliente() {
                    Codigo = (int)e.LocalAtendimento.CodPosto,
                    NomeLocal = e.LocalAtendimento.NomeLocal,
                    Agencia = e.LocalAtendimento.NumAgencia,
                    Posto = e.LocalAtendimento.DCPosto,
                    Endereco = e.LocalAtendimento.Endereco
                }
            }).ToList();

            return equipamentos;
        }

        public List<IntegracaoCliente> ObterMeusIncidentes(IntegracaoClienteParameters par)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(par.Chave);

            var osParams = new OrdemServicoParameters { 
                CodCliente = codCliente,
                IndIntegracao = 1,
                //DataHoraInicioInicio = new DateTime(DateTime.Now.Year, 1, 1),
                //DataHoraInicioFim = new DateTime(DateTime.Now.Year, 12, 31)
            };

            var oss = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(osParams).Items;

            var incidentes = oss.Select(os => new IntegracaoCliente { 
                NumIncidentePerto = os.CodOS.ToString(),
                NumIncidenteCliente = os.NumOSCliente,
                RelatoCliente = os.DefeitoRelatado,
                NumSerie = os.EquipamentoContrato?.NumSerie,
                Equipamento = new EquipamentoCliente() {
                    NumSerie = os.EquipamentoContrato?.NumSerie,
                    Codigo = os.EquipamentoContrato.CodEquipContrato,
                    Local = new LocalAtendimentoCliente() {
                        Codigo = (int)os.LocalAtendimento.CodPosto,
                        NomeLocal = os.LocalAtendimento.NomeLocal,
                        Agencia = os.LocalAtendimento.NumAgencia,
                        Posto = os.LocalAtendimento.DCPosto,
                        Endereco = os.LocalAtendimento.Endereco
                    }
                }
            }).ToList();

            return incidentes;
        }
    }
}