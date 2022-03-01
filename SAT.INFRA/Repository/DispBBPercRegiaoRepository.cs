using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class DispBBPercRegiaoRepository : IDispBBPercRegiaoRepository
    {
        private readonly AppDbContext _context;

        public DispBBPercRegiaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DispBBPercRegiao> ObterPorParametros(DispBBPercRegiaoParameters parameters)
        {
            var perc = _context.DispBBPercRegiao
                .AsQueryable();

            if (parameters.IndAtivo.HasValue)
                perc = perc.Where(c => c.IndAtivo == parameters.IndAtivo.Value);

            return perc.ToList();
        }
    }
}