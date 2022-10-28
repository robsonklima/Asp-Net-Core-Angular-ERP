using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORCheckListRepository : IORCheckListRepository
    {
        private readonly AppDbContext _context;

        public ORCheckListRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORCheckList checkList)
        {
            _context.ChangeTracker.Clear();
            ORCheckList c = _context.ORCheckList.FirstOrDefault(c => c.CodORChecklist == checkList.CodORChecklist);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(checkList);
                _context.SaveChanges();
            }
        }

        public void Criar(ORCheckList checkList)
        {
            _context.Add(checkList);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORCheckList c = _context.ORCheckList.FirstOrDefault(c => c.CodORChecklist == cod);

            if (c != null)
            {
                _context.ORCheckList.Remove(c);
                _context.SaveChanges();
            }
        }

        public ORCheckList ObterPorCodigo(int cod)
        {
            return _context.ORCheckList
                .FirstOrDefault(c => c.CodORChecklist == cod);
        }

        public PagedList<ORCheckList> ObterPorParametros(ORCheckListParameters parameters)
        {
            var query = _context.ORCheckList
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                query = query.Where(
                    s =>
                    s.CodORChecklist.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );

            if (parameters.SortActive != null && parameters.SortDirection != null)
                 query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ORCheckList>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
