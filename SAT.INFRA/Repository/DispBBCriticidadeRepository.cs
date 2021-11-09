using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Repository
{
    public class DispBBCriticidadeRepository : IDispBBCriticidadeRepository
    {
        private readonly AppDbContext _context;

        public DispBBCriticidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DispBBCriticidade> ObterPorParametros(DispBBCriticidadeParameters parameters)
        {
            var criticidades = _context.DispBBCriticidade
                .AsQueryable();

            if (parameters.IndAtivo.HasValue)
                criticidades = criticidades.Where(c => c.IndAtivo == parameters.IndAtivo.Value);

            return criticidades.ToList();
        }
    }
}