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
            var desvios = _context.DispBBDesvio
                .AsQueryable();

            if (parameters.IndAtivo.HasValue)
                desvios = desvios.Where(c => c.IndAtivo == parameters.IndAtivo.Value);

            return desvios.ToList();
        }
    }
}