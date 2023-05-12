using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RedeBanrisulRepository : IRedeBanrisulRepository
    {
        private readonly AppDbContext _context;
        public RedeBanrisulRepository(AppDbContext context)
        {
            this._context = context;
        }

        public RedeBanrisul Atualizar(RedeBanrisul rede)
        {
            _context.ChangeTracker.Clear();
            RedeBanrisul t = _context.RedeBanrisul.FirstOrDefault(t => t.CodRedeBanrisul == rede.CodRedeBanrisul);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(rede);
                _context.SaveChanges();
            }

            return t;
        }

        public RedeBanrisul Criar(RedeBanrisul rede)
        {
            _context.Add(rede);
            _context.SaveChanges();
            return rede;
        }

        public RedeBanrisul Deletar(int codRedeBanrisul)
        {
            RedeBanrisul t = _context.RedeBanrisul.FirstOrDefault(t => t.CodRedeBanrisul == codRedeBanrisul);

            if (t != null)
            {
                _context.RedeBanrisul.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public RedeBanrisul ObterPorCodigo(int codigo)
        {
            return _context.RedeBanrisul
                .FirstOrDefault(t => t.CodRedeBanrisul == codigo);
        }

        public PagedList<RedeBanrisul> ObterPorParametros(RedeBanrisulParameters parameters)
        {
            var query = _context.RedeBanrisul.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<RedeBanrisul>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
