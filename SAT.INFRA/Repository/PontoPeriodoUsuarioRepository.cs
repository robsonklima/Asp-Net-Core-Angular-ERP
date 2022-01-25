using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PontoPeriodoUsuarioRepository : IPontoPeriodoUsuarioRepository
    {
        private readonly AppDbContext _context;

        public PontoPeriodoUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _context.ChangeTracker.Clear();
            PontoPeriodoUsuario per = _context.PontoPeriodoUsuario.SingleOrDefault(p => p.CodPontoPeriodoUsuario == pontoPeriodoUsuario.CodPontoPeriodoUsuario);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(pontoPeriodoUsuario);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            try
            {
                _context.Add(pontoPeriodoUsuario);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            PontoPeriodoUsuario per = _context.PontoPeriodoUsuario.SingleOrDefault(p => p.CodPontoPeriodoUsuario == codigo);

            if (per != null)
            {
                _context.PontoPeriodoUsuario.Remove(per);

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

        public PontoPeriodoUsuario ObterPorCodigo(int codigo)
        {
            return _context.PontoPeriodoUsuario.SingleOrDefault(p => p.CodPontoPeriodoUsuario == codigo);
        }

        public PagedList<PontoPeriodoUsuario> ObterPorParametros(PontoPeriodoUsuarioParameters parameters)
        {
            var query = _context.PontoPeriodoUsuario
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoPeriodoUsuario.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            var a = query.ToQueryString();

            return PagedList<PontoPeriodoUsuario>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
