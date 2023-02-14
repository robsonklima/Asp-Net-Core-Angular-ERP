using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PontoPeriodoUsuarioStatusRepository : IPontoPeriodoUsuarioStatusRepository
    {
        private readonly AppDbContext _context;

        public PontoPeriodoUsuarioStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            _context.ChangeTracker.Clear();
            PontoPeriodoUsuarioStatus per = _context.PontoPeriodoUsuarioStatus.SingleOrDefault(p => p.CodPontoPeriodoUsuarioStatus == pontoPeriodoUsuarioStatus.CodPontoPeriodoUsuarioStatus);

            if (per != null)
            {
                try
                {
                    _context.Entry(per).CurrentValues.SetValues(pontoPeriodoUsuarioStatus);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            try
            {
                _context.Add(pontoPeriodoUsuarioStatus);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            PontoPeriodoUsuarioStatus per = _context.PontoPeriodoUsuarioStatus.SingleOrDefault(p => p.CodPontoPeriodoUsuarioStatus == codigo);

            if (per != null)
            {
                _context.PontoPeriodoUsuarioStatus.Remove(per);

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

        public PontoPeriodoUsuarioStatus ObterPorCodigo(int codigo)
        {
            return _context.PontoPeriodoUsuarioStatus.SingleOrDefault(p => p.CodPontoPeriodoUsuarioStatus == codigo);
        }

        public PagedList<PontoPeriodoUsuarioStatus> ObterPorParametros(PontoPeriodoUsuarioStatusParameters parameters)
        {
            var query = _context.PontoPeriodoUsuarioStatus
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodPontoPeriodoUsuarioStatus.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodPontoPeriodoUsuarioStatus != null)
            {
                query = query.Where(c => c.CodPontoPeriodoUsuarioStatus == parameters.CodPontoPeriodoUsuarioStatus);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Descricao))
            {
                query = query.Where(c => c.Descricao == parameters.Descricao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            var a = query.ToQueryString();

            return PagedList<PontoPeriodoUsuarioStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
