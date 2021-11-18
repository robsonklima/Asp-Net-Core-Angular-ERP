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
            var instalacoes = _context.InstalacaoLote
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(p =>
                    p.CodInstalLote.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<InstalacaoLote>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
