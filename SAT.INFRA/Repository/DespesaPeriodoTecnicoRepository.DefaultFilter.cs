using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroPadrao(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    t =>
                    t.DespesaProtocoloPeriodoTecnico.CodDespesaPeriodoTecnico.ToString().Contains(parameters.Filter) ||
                    t.CodDespesaPeriodoTecnico.ToString().Contains(parameters.Filter));

            if (parameters.CodDespesaPeriodo.HasValue)
                query = query.Where(e => e.CodDespesaPeriodo == parameters.CodDespesaPeriodo);

            if (!string.IsNullOrEmpty(parameters.CodTecnico))
            {
                var codigos = parameters.CodTecnico.Split(',').Select(f => f.Trim());
                query = query.Where(e => codigos.Any(p => p == e.CodTecnico.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodFilial))
            {
                var codigos = parameters.CodFilial.Split(',').Select(f => f.Trim());
                query = query.Where(e => codigos.Any(p => p == e.Tecnico.CodFilial.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodDespesaProtocolo))
            {
                var codigos = parameters.CodDespesaProtocolo.Split(',').Select(f => f.Trim());
                query = query.Where(e => codigos.Any(p => p == e.DespesaProtocoloPeriodoTecnico.CodDespesaProtocolo.ToString()));
            }

            if (parameters.IndAtivoPeriodo.HasValue)
                query = query.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            if (!string.IsNullOrEmpty(parameters.CodDespesaPeriodoStatus))
            {
                var codigos = parameters.CodDespesaPeriodoStatus.Split(',').Select(f => f.Trim());
                query = query.Where(e => codigos.Any(p => p == e.CodDespesaPeriodoTecnicoStatus.ToString()));
            }

            return query;
        }
    }
}