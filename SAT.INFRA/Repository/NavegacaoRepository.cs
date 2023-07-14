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
    public class NavegacaoRepository : INavegacaoRepository
    {
        private readonly AppDbContext _context;

        public NavegacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Navegacao navegacao)
        {
            _context.ChangeTracker.Clear();
            Navegacao per = _context.Navegacao.SingleOrDefault(p => p.CodNavegacao == navegacao.CodNavegacao);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(navegacao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(Navegacao navegacao)
        {
            try
            {
                _context.Add(navegacao);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            Navegacao per = _context.Navegacao.SingleOrDefault(p => p.CodNavegacao == codigo);

            if (per != null)
            {
                _context.Navegacao.Remove(per);

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

        public Navegacao ObterPorCodigo(int codigo)
        {
            return _context.Navegacao.SingleOrDefault(p => p.CodNavegacao == codigo);
        }

        public PagedList<Navegacao> ObterPorParametros(NavegacaoParameters parameters)
        {
            var perfis = _context.Navegacao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodNavegacao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.CodNavegacaoPai.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) 

                );
            }

            if (parameters.CodNavegacao != null)
            {
                perfis = perfis.Where(p => p.CodNavegacao == parameters.CodNavegacao);
            }

            if (parameters.CodNavegacaoPai != null)
            {
                perfis = perfis.Where(p => p.CodNavegacaoPai == parameters.CodNavegacaoPai);
            }

            if (parameters.IndAtivo != null)
            {
                perfis = perfis.Where(p => p.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Navegacao>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
