using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RecursoBloqueadoRepository : IRecursoBloqueadoRepository
    {
        private readonly AppDbContext _context;
        public RecursoBloqueadoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public RecursoBloqueado Atualizar(RecursoBloqueado rec)
        {
            _context.ChangeTracker.Clear();
            RecursoBloqueado t = _context.RecursoBloqueado.FirstOrDefault(t => t.CodRecursoBloqueado == rec.CodRecursoBloqueado);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(rec);
                _context.SaveChanges();
            }

            return t;
        }

        public RecursoBloqueado Criar(RecursoBloqueado rec)
        {
            _context.Add(rec);
            _context.SaveChanges();
            return rec;
        }

        public RecursoBloqueado Deletar(int cod)
        {
            RecursoBloqueado t = _context.RecursoBloqueado.FirstOrDefault(t => t.CodRecursoBloqueado == cod);

            if (t != null)
            {
                _context.RecursoBloqueado.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public RecursoBloqueado ObterPorCodigo(int codigo)
        {
            return _context.RecursoBloqueado
                .FirstOrDefault(t => t.CodRecursoBloqueado == codigo);
        }

        public PagedList<RecursoBloqueado> ObterPorParametros(RecursoBloqueadoParameters parameters)
        {
            var query = _context.RecursoBloqueado
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<RecursoBloqueado>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
