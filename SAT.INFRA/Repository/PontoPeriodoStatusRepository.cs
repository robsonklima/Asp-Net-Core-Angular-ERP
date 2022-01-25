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
    public class PontoPeriodoStatusRepository : IPontoPeriodoStatusRepository
    {
        private readonly AppDbContext _context;

        public PontoPeriodoStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoPeriodoStatus pontoPeriodoStatus)
        {
            PontoPeriodoStatus per = _context.PontoPeriodoStatus.SingleOrDefault(p => p.CodPontoPeriodoStatus == pontoPeriodoStatus.CodPontoPeriodoStatus);

            if (per != null)
            {
                _context.Entry(per).CurrentValues.SetValues(pontoPeriodoStatus);

                try
                {
                    _context.ChangeTracker.Clear();
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(PontoPeriodoStatus pontoPeriodoStatus)
        {
            try
            {
                _context.Add(pontoPeriodoStatus);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            PontoPeriodoStatus per = _context.PontoPeriodoStatus.SingleOrDefault(p => p.CodPontoPeriodoStatus == codigo);

            if (per != null)
            {
                _context.PontoPeriodoStatus.Remove(per);

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

        public PontoPeriodoStatus ObterPorCodigo(int codigo)
        {
            return _context.PontoPeriodoStatus.SingleOrDefault(p => p.CodPontoPeriodoStatus == codigo);
        }

        public PagedList<PontoPeriodoStatus> ObterPorParametros(PontoPeriodoStatusParameters parameters)
        {
            var query = _context.PontoPeriodoStatus
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoPeriodoStatus.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            var a = query.ToQueryString();

            return PagedList<PontoPeriodoStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
