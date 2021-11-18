using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoRepository : IInstalacaoRepository
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
            var instalacoes = _context.Instalacao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(p =>
                    p.CodInstalacao.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Instalacao>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
