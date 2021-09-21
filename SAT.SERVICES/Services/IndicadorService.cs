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

        public IndicadorService(IOrdemServicoRepository osRepository)
        {
            _osRepository = osRepository;
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
                PageSize = Int32.MaxValue
            }).Where(w => w.PrazosAtendimento.Count > 0);
        }


        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            switch (parameters.Tipo)
            {
                case IndicadorTipoEnum.ORDEM_SERVICO:
                    return ObterIndicadorOrdemServico(parameters);
                case IndicadorTipoEnum.SLA:
                    return ObterIndicadorSLA(parameters);
                case IndicadorTipoEnum.PENDENCIA:
                    return ObterIndicadorPendencia(parameters);
                case IndicadorTipoEnum.REINCIDENCIA:
                    return ObterIndicadorReincidencia(parameters);
                case IndicadorTipoEnum.SPA:
                    return ObterIndicadorSPA(parameters);
                default:
                    throw new NotImplementedException("Não Implementado");
            }
        }
    }
}
