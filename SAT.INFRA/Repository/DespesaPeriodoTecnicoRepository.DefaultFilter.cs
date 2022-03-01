using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities.Params;

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
                int[] codigos = parameters.CodTecnico.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(e => codigos.Contains(e.CodTecnico));
            }

            if (!string.IsNullOrEmpty(parameters.CodFilial))
            {
                int[] codigos = parameters.CodFilial.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(e => codigos.Contains(e.Tecnico.CodFilial.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodDespesaProtocolo))
            {
                query = query.Where(e => e.DespesaProtocoloPeriodoTecnico.CodDespesaProtocolo.ToString()
                             .Contains(parameters.CodDespesaProtocolo));
            }

            if (parameters.IndAtivoPeriodo.HasValue)
                query = query.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            if (!string.IsNullOrEmpty(parameters.CodDespesaPeriodoStatus))
            {
                int[] codigos = parameters.CodDespesaPeriodoStatus.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(e => codigos.Contains(e.CodDespesaPeriodoTecnicoStatus));
            }

            return query;
        }
    }
}