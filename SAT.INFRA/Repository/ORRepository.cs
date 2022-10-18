using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class ORRepository : IORRepository
    {
        private readonly AppDbContext _context;

        public ORRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OR or)
        {
            _context.ChangeTracker.Clear();
            OR p = _context.OR.FirstOrDefault(p => p.CodOR == or.CodOR);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(or);
                _context.SaveChanges();
            }
        }

        public void Criar(OR or)
        {
            _context.Add(or);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            OR OR = _context.OR.FirstOrDefault(p => p.CodOR == codigo);

            if (OR != null)
            {
                _context.OR.Remove(OR);
                _context.SaveChanges();
            }
        }

        public OR ObterPorCodigo(int codigo)
        {
            return _context.OR
                .Include(or => or.ORStatus)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.Peca)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.StatusOR)
                .FirstOrDefault(p => p.CodOR == codigo);
        }

        public PagedList<OR> ObterPorParametros(ORParameters parameters)
        {
            var ORes = _context.OR
                .Include(or => or.ORItens)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                ORes = ORes.Where(
                    p =>
                    p.CodOR.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                ORes = ORes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OR>.ToPagedList(ORes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
