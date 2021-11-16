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
            if (parameters.CodDespesaPeriodo.HasValue)
                query = query.Where(e => e.CodDespesaPeriodo == parameters.CodDespesaPeriodo);

            if (parameters.CodTecnico.HasValue)
                query = query.Where(e => e.CodTecnico == parameters.CodTecnico);

            if (parameters.IndAtivoPeriodo.HasValue)
                query = query.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            return query;
        }
    }
}