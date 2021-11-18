using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoLoteRepository : IInstalacaoLoteRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoLoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoLote instalacaoLote)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(InstalacaoLote instalacaoLote)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public InstalacaoLote ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoLote.FirstOrDefault(f => f.CodInstalLote == codigo);
        }

        public PagedList<InstalacaoLote> ObterPorParametros(InstalacaoLoteParameters parameters)
        {
            var query = _context.InstalacaoLote
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodInstalLote.ToString().Contains(parameters.Filter) ||
                    p.NomeLote.Contains(parameters.Filter) ||
                    p.DescLote.Contains(parameters.Filter)
                );
            }

            if (parameters.CodContrato != null)
            {
                query = query.Where(l => l.CodContrato == parameters.CodContrato);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<InstalacaoLote>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
