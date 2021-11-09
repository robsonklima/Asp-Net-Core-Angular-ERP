using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Repository
{
    public class DispBBDesvioRepository : IDispBBDesvioRepository
    {
        private readonly AppDbContext _context;

        public DispBBDesvioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DispBBDesvio> ObterPorParametros(DispBBDesvioParameters parameters)
        {
            return null;
        }
    }
}