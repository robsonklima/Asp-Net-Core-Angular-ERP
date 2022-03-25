using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DefeitoComponenteRepository : IDefeitoComponenteRepository
    {
        private readonly AppDbContext _context;

        public DefeitoComponenteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DefeitoComponente ExisteDefeitoComponente(DefeitoComponente defeitoComponente)
        {
            defeitoComponente.CodDefeitoComponente =
            _context.DefeitoComponente.FirstOrDefault(a => a.CodECausa == defeitoComponente.CodECausa && a.CodDefeito == defeitoComponente.CodDefeito)?.CodDefeitoComponente ?? 0;
            return defeitoComponente;
        }

        public void Atualizar(DefeitoComponente defeitoComponente)
        {
            _context.ChangeTracker.Clear();
            DefeitoComponente a = _context.DefeitoComponente.SingleOrDefault(a => a.CodDefeitoComponente == defeitoComponente.CodDefeitoComponente);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(defeitoComponente);
                    _context.Entry(a).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(DefeitoComponente defeitoComponente)
        {
            try
            {
                _context.Add(defeitoComponente);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            DefeitoComponente a = _context.DefeitoComponente.SingleOrDefault(a => a.CodDefeitoComponente == codigo);

            if (a != null)
            {
                try
                {
                    _context.DefeitoComponente.Remove(a);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public DefeitoComponente ObterPorCodigo(int codigo)
        {
            return _context.DefeitoComponente
                .Include(i => i.Defeito).Where(ac => ac.Defeito.IndAtivo == 1)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .SingleOrDefault(a => a.CodDefeitoComponente == codigo);
        }

        public PagedList<DefeitoComponente> ObterPorParametros(DefeitoComponenteParameters parameters)
        {
            var defeitoComponente = _context.DefeitoComponente
                .Include(i => i.Defeito).Where(ac => ac.Defeito.IndAtivo == 1)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                defeitoComponente = defeitoComponente.Where(
                    c =>
                    c.Defeito.NomeDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.Defeito.CodEDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodDefeitoComponente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodECausa))
            {
                defeitoComponente = defeitoComponente.Where(w => w.CodECausa == parameters.CodECausa);
            }

            return PagedList<DefeitoComponente>.ToPagedList(defeitoComponente, parameters.PageNumber, parameters.PageSize);
        }
    }
}
