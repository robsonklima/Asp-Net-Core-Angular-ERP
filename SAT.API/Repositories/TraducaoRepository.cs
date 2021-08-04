using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SAT.API.Repositories
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
