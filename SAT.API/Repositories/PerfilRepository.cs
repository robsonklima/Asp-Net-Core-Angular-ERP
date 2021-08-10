using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.API.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly AppDbContext _context;

        public PerfilRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Perfil perfil)
        {
            Perfil per = _context.Perfil.SingleOrDefault(p => p.CodPerfil == perfil.CodPerfil);

            if (per != null)
            {
                _context.Entry(per).CurrentValues.SetValues(perfil);

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

        public void Criar(Perfil perfil)
        {
            try
            {
                _context.Add(perfil);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            Perfil per = _context.Perfil.SingleOrDefault(p => p.CodPerfil == codigo);

            if (per != null)
            {
                _context.Perfil.Remove(per);

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

        public Perfil ObterPorCodigo(int codigo)
        {
            return _context.Perfil.SingleOrDefault(p => p.CodPerfil == codigo);
        }

        public PagedList<Perfil> ObterPorParametros(PerfilParameters parameters)
        {
            var perfis = _context.Perfil
                .AsQueryable();

            if (parameters.Filter != null)
            {
                perfis = perfis.Where(
                    p =>
                    p.CodPerfil.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.NomePerfil.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (parameters.CodPerfil != null)
            {
                perfis = perfis.Where(p => p.CodPerfil == parameters.CodPerfil);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                perfis = perfis.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Perfil>.ToPagedList(perfis, parameters.PageNumber, parameters.PageSize);
        }
    }
}
