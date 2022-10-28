using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class ORCheckListItemRepository : IORCheckListItemRepository
    {
        private readonly AppDbContext _context;

        public ORCheckListItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORCheckListItem item)
        {
            _context.ChangeTracker.Clear();
            ORCheckListItem c = _context.ORCheckListItem.FirstOrDefault(c => c.CodORCheckListItem == item.CodORCheckListItem);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
        }

        public void Criar(ORCheckListItem item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORCheckListItem c = _context.ORCheckListItem.FirstOrDefault(c => c.CodORCheckListItem == cod);

            if (c != null)
            {
                _context.ORCheckListItem.Remove(c);
                _context.SaveChanges();
            }
        }

        public ORCheckListItem ObterPorCodigo(int cod)
        {
            return _context.ORCheckListItem
                .Include(i => i.Peca)
                .FirstOrDefault(c => c.CodORCheckListItem == cod);
        }

        public PagedList<ORCheckListItem> ObterPorParametros(ORCheckListItemParameters parameters)
        {
            var query = _context.ORCheckListItem
                .Include(i => i.Peca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    s =>
                    s.CodORCheckListItem.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (parameters.SortActive != null && parameters.SortDirection != null)
                 query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ORCheckListItem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
