using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.API.Repositories
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
            Navegacao n = _context.Navegacao.SingleOrDefault(n => n.CodNavegacao == navegacao.CodNavegacao);

            if (n != null)
            {
                _context.Entry(n).CurrentValues.SetValues(navegacao);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
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
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            Navegacao navegacao = _context.Navegacao.SingleOrDefault(n => n.CodNavegacao == codigo);

            if (navegacao != null)
            {
                _context.Navegacao.Remove(navegacao);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public Navegacao ObterPorCodigo(int codigo)
        {
            return _context.Navegacao.FirstOrDefault(f => f.CodNavegacao == codigo);
        }

        public PagedList<Navegacao> ObterPorParametros(NavegacaoParameters parameters)
        {
            var navs = _context.Navegacao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                navs = navs.Where(
                    n =>
                    n.CodNavegacao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    n.Title.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodNavegacao != null)
            {
                navs = navs.Where(n => n.CodNavegacao == parameters.CodNavegacao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                navs = navs.OrderBy(parameters.SortActive, parameters.SortDirection);
            }

            return PagedList<Navegacao>.ToPagedList(navs, parameters.PageNumber, parameters.PageSize);
        }
    }
}
