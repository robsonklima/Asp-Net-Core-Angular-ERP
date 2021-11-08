using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class InstalacaoRepository : IInstalacaoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Instalacao instalacao)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(Instalacao instalacao)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public Instalacao ObterPorCodigo(int codigo)
        {
            return _context.Instalacao.FirstOrDefault(f => f.CodInstalacao == codigo);
        }

        public PagedList<Instalacao> ObterPorParametros(InstalacaoParameters parameters)
        {
            var query = _context.Instalacao
                .Include(i => i.InstalLote)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                        f =>
                        f.CodInstalacao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) 
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Instalacao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
