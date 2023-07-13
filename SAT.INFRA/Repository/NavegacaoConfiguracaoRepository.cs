using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class NavegacaoConfiguracaoRepository : INavegacaoConfiguracaoRepository
    {
        private readonly AppDbContext _context;

        public NavegacaoConfiguracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(NavegacaoConfiguracao navegacaoConfiguracao)
        {
            _context.ChangeTracker.Clear();
            NavegacaoConfiguracao per = _context.NavegacaoConfiguracao.SingleOrDefault(p => p.CodNavegacaoConfiguracao == navegacaoConfiguracao.CodNavegacaoConfiguracao);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(navegacaoConfiguracao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(NavegacaoConfiguracao navegacaoConfiguracao)
        {
            try
            {
                _context.Add(navegacaoConfiguracao);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            NavegacaoConfiguracao per = _context.NavegacaoConfiguracao.SingleOrDefault(p => p.CodNavegacaoConfiguracao == codigo);

            if (per != null)
            {
                _context.NavegacaoConfiguracao.Remove(per);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public NavegacaoConfiguracao ObterPorCodigo(int codigo)
        {
            return _context.NavegacaoConfiguracao.SingleOrDefault(p => p.CodNavegacaoConfiguracao == codigo);
        }

        public PagedList<NavegacaoConfiguracao> ObterPorParametros(NavegacaoConfiguracaoParameters parameters)
        {
            var perfis = _context.NavegacaoConfiguracao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodNavegacaoConfiguracao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (parameters.CodNavegacao != null)
            {
                perfis = perfis.Where(p => p.CodNavegacao == parameters.CodNavegacao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<NavegacaoConfiguracao>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
