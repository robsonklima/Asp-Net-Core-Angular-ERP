using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;
        private readonly IDispBBCriticidadeRepository _dispBBCriticidadeRepository;
        private readonly IFeriadoService _feriadoService;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepository;
        private readonly IDispBBRegiaoFilialRepository _dispBBRegiaoFilialRepository;
        private readonly IDispBBPercRegiaoRepository _dispBBPercRegiaoRepository;
        private readonly IDispBBDesvioRepository _dispBBDesvioRepository;

        public IndicadorService(IOrdemServicoRepository osRepository,
            IFeriadoService feriadoService,
            IEquipamentoContratoRepository equipamentoContratoRepository,
            IDispBBRegiaoFilialRepository dispBBRegiaoFilialRepository,
            IDispBBCriticidadeRepository dispBBCriticidadeRepository,
            IDispBBPercRegiaoRepository dispBBPercRegiaoRepository,
            IDispBBDesvioRepository dispBBDesvioRepository)
        {
            _osRepository = osRepository;
            _feriadoService = feriadoService;
            _dispBBCriticidadeRepository = dispBBCriticidadeRepository;
            _equipamentoContratoRepository = equipamentoContratoRepository;
            _dispBBRegiaoFilialRepository = dispBBRegiaoFilialRepository;
            _dispBBPercRegiaoRepository = dispBBPercRegiaoRepository;
            _dispBBDesvioRepository = dispBBDesvioRepository;
        }

        private IEnumerable<OrdemServico> ObterOrdensServico(IndicadorParameters parameters)
        {
            return _osRepository.ObterPorParametros(new OrdemServicoParameters()
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
                PageSize = Int32.MaxValue
            }).Where(w => w.PrazosAtendimento.Count > 0);
        }

        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            return parameters.Tipo switch
            {
                IndicadorTipoEnum.ORDEM_SERVICO => ObterIndicadorOrdemServico(parameters),
                IndicadorTipoEnum.SLA => ObterIndicadorSLA(parameters),
                IndicadorTipoEnum.PENDENCIA => ObterIndicadorPendencia(parameters),
                IndicadorTipoEnum.REINCIDENCIA => ObterIndicadorReincidencia(parameters),
                IndicadorTipoEnum.SPA => ObterIndicadorSPA(parameters),
                IndicadorTipoEnum.PECA_FALTANTE => ObterIndicadorPecaFaltante(parameters),
                IndicadorTipoEnum.DISPONIBILIDADE => ObterIndicadorDisponibilidade(parameters),
                _ => throw new NotImplementedException("Não Implementado"),
            };
        }

        public List<Indicador> ObterIndicadoresFiliais()
        {
            return null;
        }
    }
}
