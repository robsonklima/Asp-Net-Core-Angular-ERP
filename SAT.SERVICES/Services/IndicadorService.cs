using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;
        private readonly IDashboardService _dashboardService;
        private readonly IDispBBCriticidadeRepository _dispBBCriticidadeRepository;
        private readonly IFeriadoService _feriadoService;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepository;
        private readonly IDispBBRegiaoFilialRepository _dispBBRegiaoFilialRepository;
        private readonly IDispBBPercRegiaoRepository _dispBBPercRegiaoRepository;
        private readonly IDispBBDesvioRepository _dispBBDesvioRepository;
        private readonly ITecnicoRepository _tecnicosRepo;

        public IndicadorService(IOrdemServicoRepository osRepository,
            IFeriadoService feriadoService,
            IEquipamentoContratoRepository equipamentoContratoRepository,
            IDispBBRegiaoFilialRepository dispBBRegiaoFilialRepository,
            IDispBBCriticidadeRepository dispBBCriticidadeRepository,
            IDispBBPercRegiaoRepository dispBBPercRegiaoRepository,
            IDispBBDesvioRepository dispBBDesvioRepository,
            IDashboardService dashboardService,
             ITecnicoRepository tecnicosRepo)
        {
            _osRepository = osRepository;
            _feriadoService = feriadoService;
            _dispBBCriticidadeRepository = dispBBCriticidadeRepository;
            _equipamentoContratoRepository = equipamentoContratoRepository;
            _dispBBRegiaoFilialRepository = dispBBRegiaoFilialRepository;
            _dispBBPercRegiaoRepository = dispBBPercRegiaoRepository;
            _dispBBDesvioRepository = dispBBDesvioRepository;
            _dashboardService = dashboardService;
            _tecnicosRepo = tecnicosRepo;
        }

        private IEnumerable<OrdemServico> ObterOrdensServico(IndicadorParameters parameters)
        {
            return _osRepository.ObterQuery(new OrdemServicoParameters()
            {
                DataAberturaInicio = parameters.DataInicio,
                DataAberturaFim = parameters.DataFim,
                CodClientes = parameters.CodClientes,
                CodFiliais = parameters.CodFiliais,
                CodStatusServicos = parameters.CodStatusServicos,
                CodTiposIntervencao = parameters.CodTiposIntervencao,
                CodAutorizadas = parameters.CodAutorizadas,
                CodTiposGrupo = parameters.CodTiposGrupo,
                Include = parameters.Include,
                PageSize = Int32.MaxValue,
                FilterType = parameters.FilterType
            }).Where(w => w.PrazosAtendimento.Count > 0);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDisponibilidadeTecnicos(IndicadorParameters parameters)
        {
            return ObterIndicadorDisponibilidadeTecnicos(parameters);
        }

        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            switch (parameters.Tipo)
            {
                case IndicadorTipoEnum.ORDEM_SERVICO:
                    return ObterIndicadorOrdemServico(parameters);
                case IndicadorTipoEnum.SLA:
                    return ObterIndicadorSLA(parameters);
                case IndicadorTipoEnum.SPA:
                    return ObterIndicadorSPA(parameters);
                case IndicadorTipoEnum.PENDENCIA:
                    return ObterIndicadorPendencia(parameters);
                case IndicadorTipoEnum.REINCIDENCIA:
                    return ObterIndicadorReincidencia(parameters);
                case IndicadorTipoEnum.PECA_FALTANTE:
                    return ObterIndicadorPecaFaltante(parameters);
                case IndicadorTipoEnum.DISPONIBILIDADE: // Ver depois o BB 
                    return ObterIndicadorDisponibilidade(parameters);
                case IndicadorTipoEnum.PERFORMANCE_FILIAIS:
                    return ObterDadosIndicador(parameters);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Faz o agrupamento dos Indicadores - Alguns dados precisam ser agrupados por periodo
        /// </summary>
        /// <param name="nomeIndicador"></param>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <returns></returns>
        private List<Indicador> AgrupadorIndicador(string nomeIndicador, DateTime dataInicio, DateTime dataFim)
        {
            return (from ind in _dashboardService.ObterDadosIndicador(nomeIndicador, dataInicio, dataFim)
                    group ind by ind.Label into grupo
                    select new Indicador
                    {
                        Label = grupo.Key,
                        Valor = grupo.Sum(s => s.Valor) / grupo.Count(),
                        Filho = grupo.FirstOrDefault().Filho
                    }).ToList();
        }
    }
}
