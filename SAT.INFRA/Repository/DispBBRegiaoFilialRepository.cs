using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Repository
{
    public class DispBBRegiaoFilialRepository : IDispBBRegiaoFilialRepository
    {
        private readonly AppDbContext _context;

        public DispBBRegiaoFilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DispBBRegiaoFilial> ObterPorParametros(DispBBRegiaoFilialParameters parameters)
        {
            return null;
        }
    }
}