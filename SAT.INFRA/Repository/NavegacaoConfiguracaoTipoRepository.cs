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
    public class NavegacaoConfiguracaoTipoRepository : INavegacaoConfiguracaoTipoRepository
    {
        private readonly AppDbContext _context;

        public NavegacaoConfiguracaoTipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo)
        {
            _context.ChangeTracker.Clear();
            NavegacaoConfiguracaoTipo per = _context.NavegacaoConfiguracaoTipo.SingleOrDefault(p => p.CodNavegacaoConfTipo == navegacaoConfiguracaoTipo.CodNavegacaoConfTipo);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(navegacaoConfiguracaoTipo);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo)
        {
            try
            {
                _context.Add(navegacaoConfiguracaoTipo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            NavegacaoConfiguracaoTipo per = _context.NavegacaoConfiguracaoTipo.SingleOrDefault(p => p.CodNavegacaoConfTipo == codigo);

            if (per != null)
            {
                _context.NavegacaoConfiguracaoTipo.Remove(per);

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

        public NavegacaoConfiguracaoTipo ObterPorCodigo(int codigo)
        {
            return _context.NavegacaoConfiguracaoTipo.SingleOrDefault(p => p.CodNavegacaoConfTipo == codigo);
        }

        public PagedList<NavegacaoConfiguracaoTipo> ObterPorParametros(NavegacaoConfiguracaoTipoParameters parameters)
        {
            var perfis = _context.NavegacaoConfiguracaoTipo
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodNavegacaoConfTipo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (parameters.CodNavegacaoConfTipo != null)
            {
                perfis = perfis.Where(p => p.CodNavegacaoConfTipo == parameters.CodNavegacaoConfTipo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<NavegacaoConfiguracaoTipo>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
