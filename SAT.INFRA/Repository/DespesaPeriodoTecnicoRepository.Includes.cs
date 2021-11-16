using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarIncludes(IQueryable<DespesaPeriodoTecnico> query)
        {
            query = query
                .Include(dpt => dpt.DespesaPeriodo)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                .Include(dpt => dpt.DespesaPeriodoTecnicoStatus)
                .AsQueryable();

            return query;
        }
    }
}