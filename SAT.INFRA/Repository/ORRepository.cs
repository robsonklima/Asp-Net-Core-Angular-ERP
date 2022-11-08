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

        public OR Criar(OR or)
        {
            _context.Add(or);
            _context.SaveChanges();
            return or;
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
                .Include(or => or.Destino)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.Peca)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.StatusOR)
                .Include(or => or.ORTransporte)
                .FirstOrDefault(p => p.CodOR == codigo);
        }

        public PagedList<OR> ObterPorParametros(ORParameters parameters)
        {
            var query = _context.OR
                .Include(or => or.ORStatus)
                .Include(or => or.UsuarioCadastro)
                .Include(or => or.Destino)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.Peca)
                .Include(or => or.ORItens)
                    .ThenInclude(i => i.StatusOR)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodOR.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.ORStatus.DescStatus.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodStatus))
            {
                int[] cods = parameters.CodStatus.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(q => cods.Contains(q.CodStatusOR));
            }

            if (parameters.CodOR.HasValue)
            {
                query = query.Where(q => q.CodOR == parameters.CodOR);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OR>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
