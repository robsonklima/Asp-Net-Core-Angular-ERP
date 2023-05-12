using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class OperadoraTelefoniaRepository : IOperadoraTelefoniaRepository
    {
        private readonly AppDbContext _context;
        public OperadoraTelefoniaRepository(AppDbContext context)
        {
            this._context = context;
        }

        public OperadoraTelefonia Atualizar(OperadoraTelefonia op)
        {
            _context.ChangeTracker.Clear();
            OperadoraTelefonia t = _context.OperadoraTelefonia.FirstOrDefault(t => t.CodOperadoraTelefonia == op.CodOperadoraTelefonia);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(op);
                _context.SaveChanges();
            }

            return t;
        }

        public OperadoraTelefonia Criar(OperadoraTelefonia op)
        {
            _context.Add(op);
            _context.SaveChanges();
            return op;
        }

        public OperadoraTelefonia Deletar(int codOperadoraTelefonia)
        {
            OperadoraTelefonia t = _context.OperadoraTelefonia.FirstOrDefault(t => t.CodOperadoraTelefonia == codOperadoraTelefonia);

            if (t != null)
            {
                _context.OperadoraTelefonia.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public OperadoraTelefonia ObterPorCodigo(int codigo)
        {
            return _context.OperadoraTelefonia
                .FirstOrDefault(t => t.CodOperadoraTelefonia == codigo);
        }

        public PagedList<OperadoraTelefonia> ObterPorParametros(OperadoraTelefoniaParameters parameters)
        {
            var query = _context.OperadoraTelefonia.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<OperadoraTelefonia>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
