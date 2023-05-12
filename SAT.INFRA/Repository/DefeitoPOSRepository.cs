using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DefeitoPOSRepository : IDefeitoPOSRepository
    {
        private readonly AppDbContext _context;
        public DefeitoPOSRepository(AppDbContext context)
        {
            this._context = context;
        }

        public DefeitoPOS Atualizar(DefeitoPOS mc)
        {
            _context.ChangeTracker.Clear();
            DefeitoPOS t = _context.DefeitoPOS.FirstOrDefault(t => t.CodDefeitoPOS == mc.CodDefeitoPOS);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(mc);
                _context.SaveChanges();
            }

            return t;
        }

        public DefeitoPOS Criar(DefeitoPOS mc)
        {
            _context.Add(mc);
            _context.SaveChanges();
            return mc;
        }

        public DefeitoPOS Deletar(int codDefeitoPOS)
        {
            DefeitoPOS t = _context.DefeitoPOS.FirstOrDefault(t => t.CodDefeitoPOS == codDefeitoPOS);

            if (t != null)
            {
                _context.DefeitoPOS.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public DefeitoPOS ObterPorCodigo(int codigo)
        {
            return _context.DefeitoPOS
                .FirstOrDefault(t => t.CodDefeitoPOS == codigo);
        }

        public PagedList<DefeitoPOS> ObterPorParametros(DefeitoPOSParameters parameters)
        {
            var query = _context.DefeitoPOS.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<DefeitoPOS>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
