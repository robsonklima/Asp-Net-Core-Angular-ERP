using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroPeriodosAprovados(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {
            query = query
                .Where(i => i.CodDespesaPeriodoTecnicoStatus == (int)DespesaPeriodoTecnicoStatusEnum.APROVADO &&
                _despesaProtocoloPeriodoTecnicoRepo.ObterPorCodigoPeriodoTecnico(i.CodDespesaPeriodoTecnicoStatus) == null);

            return query;
        }
    }
}