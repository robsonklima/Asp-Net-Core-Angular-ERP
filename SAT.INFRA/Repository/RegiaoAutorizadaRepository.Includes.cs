using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        public IQueryable<RegiaoAutorizada> AplicarIncludes(IQueryable<RegiaoAutorizada> query)
        {
            return query
             .Include(ra => ra.Cidade)
             .Include(ra => ra.Filial)
             .Include(ra => ra.Autorizada)
             .Include(ra => ra.Regiao)
             .AsQueryable();
        }
    }
}