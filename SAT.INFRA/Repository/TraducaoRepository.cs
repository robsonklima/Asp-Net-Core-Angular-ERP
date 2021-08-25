using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Repositories
{
    public class TraducaoRepository : ITraducaoRepository
    {
        private readonly AppDbContext _context;

        public TraducaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Traducao> ObterPorParametros(int registros)
        {
            return _context.Traducao
                .Include(t => t.Lingua)
                .Take(registros);
        }
    }
}
