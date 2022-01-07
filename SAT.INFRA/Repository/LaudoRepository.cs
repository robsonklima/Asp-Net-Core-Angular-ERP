using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class LaudoRepository : ILaudoRepository
    {
        private readonly AppDbContext _context;

        public LaudoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Laudo ObterPorCodigo(int codigo) =>
            _context.Laudo
            .Include(l => l.LaudosSituacao)
            .SingleOrDefault(a => a.CodLaudo == codigo);
    }
}