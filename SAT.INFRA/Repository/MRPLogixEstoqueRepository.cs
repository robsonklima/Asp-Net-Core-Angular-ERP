using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class MRPLogixEstoqueRepository : IMRPLogixEstoqueRepository
    {
        private readonly AppDbContext _context;

        public MRPLogixEstoqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(MRPLogixEstoque mprLogixEstoque)
        {
            _context.ChangeTracker.Clear();
            MRPLogixEstoque p = _context.MRPLogixEstoque.FirstOrDefault(p => p.CodMRPLogixEstoque == mprLogixEstoque.CodMRPLogixEstoque);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(mprLogixEstoque);
                _context.SaveChanges();
            }
        }

        public MRPLogixEstoque Criar(MRPLogixEstoque mprLogixEstoque)
        {
            _context.Add(mprLogixEstoque);
            _context.SaveChanges();
            return mprLogixEstoque;
        }

        public void Deletar(int codigo)
        {
            MRPLogixEstoque MRPLogixEstoque = _context.MRPLogixEstoque.FirstOrDefault(p => p.CodMRPLogixEstoque == codigo);

            if (MRPLogixEstoque != null)
            {
                _context.MRPLogixEstoque.Remove(MRPLogixEstoque);
                _context.SaveChanges();
            }
        }
        public void LimparTabela()
        {
            var all = from c in _context.MRPLogixEstoque select c;

            if (all != null)
            {
                _context.MRPLogixEstoque.RemoveRange(all);
                _context.SaveChanges();
            }
        }        

        public MRPLogixEstoque ObterPorCodigo(int codigo)
        {
            return _context.MRPLogixEstoque
                .FirstOrDefault(p => p.CodMRPLogixEstoque == codigo);
        }

        public PagedList<MRPLogixEstoque> ObterPorParametros(MRPLogixEstoqueParameters parameters)
        {
            var query = _context.MRPLogixEstoque
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodMRPLogixEstoque.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodMRPLogixEstoque.HasValue)
            {
                query = query.Where(q => q.CodMRPLogixEstoque == parameters.CodMRPLogixEstoque);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<MRPLogixEstoque>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
