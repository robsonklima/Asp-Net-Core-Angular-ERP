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
    public class PontoUsuarioRepository : IPontoUsuarioRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoUsuario pontoUsuario)
        {
            PontoUsuario per = _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == pontoUsuario.CodPontoUsuario);

            if (per != null)
            {
                _context.Entry(per).CurrentValues.SetValues(pontoUsuario);

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

        public void Criar(PontoUsuario pontoUsuario)
        {
            try
            {
                _context.Add(pontoUsuario);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            PontoUsuario per = _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == codigo);

            if (per != null)
            {
                _context.PontoUsuario.Remove(per);

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

        public PontoUsuario ObterPorCodigo(int codigo)
        {
            return _context.PontoUsuario.SingleOrDefault(p => p.CodPontoUsuario == codigo);
        }

        public PagedList<PontoUsuario> ObterPorParametros(PontoUsuarioParameters parameters)
        {
            var query = _context.PontoUsuario
                .Include(p => p.Usuario)
                .Include(p => p.PontoPeriodo)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoUsuario.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUsuario != null)
            {
                query = query.Where(p => p.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.CodPontoPeriodo != null)
            {
                query = query.Where(p => p.CodPontoPeriodo == parameters.CodPontoPeriodo);
            }

            if (parameters.DataHoraRegistroInicio != DateTime.MinValue && parameters.DataHoraRegistroFim != DateTime.MinValue)
                query = query.Where(p => p.DataHoraRegistro >= parameters.DataHoraRegistroInicio && p.DataHoraRegistro <= parameters.DataHoraRegistroFim);

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<PontoUsuario>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
