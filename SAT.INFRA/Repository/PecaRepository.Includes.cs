using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class PecaRepository : IPecaRepository
    {
        public IQueryable<Peca> AplicarIncludes(IQueryable<Peca> query, PecaIncludeEnum include)
        {
            switch (include)
            {
                default:
                    query = query
                        .Include(p => p.PecaStatus)
                        .Include(p => p.PecaFamilia);
                    break;
            }

            return query;
        }
    }
}
