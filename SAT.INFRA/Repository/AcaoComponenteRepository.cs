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
    public class AcaoComponenteRepository : IAcaoComponenteRepository
    {
        private readonly AppDbContext _context;

        public AcaoComponenteRepository(AppDbContext context)
        {
            _context = context;
        }

        public AcaoComponente ExisteAcaoComponente(AcaoComponente acaoComponente)
        {
            acaoComponente.CodAcaoComponente =
            _context.AcaoComponente.FirstOrDefault(a => a.CodECausa == acaoComponente.CodECausa && a.CodAcao == acaoComponente.CodAcao)?.CodAcaoComponente ?? 0;
            return acaoComponente;
        }

        public void Atualizar(AcaoComponente acaoComponente)
        {
            _context.ChangeTracker.Clear();
            AcaoComponente a = _context.AcaoComponente.SingleOrDefault(a => a.CodAcaoComponente == acaoComponente.CodAcaoComponente);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(acaoComponente);
                    _context.Entry(a).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(AcaoComponente acaoComponente)
        {
            try
            {
                _context.Add(acaoComponente);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            AcaoComponente a = _context.AcaoComponente.SingleOrDefault(a => a.CodAcaoComponente == codigo);

            if (a != null)
            {
                try
                {
                    _context.AcaoComponente.Remove(a);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public AcaoComponente ObterPorCodigo(int codigo)
        {
            return _context.AcaoComponente
                .Include(i => i.Acao).Where(ac => ac.Acao.IndAtivo == 1)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .SingleOrDefault(a => a.CodAcaoComponente == codigo);
        }

        public PagedList<AcaoComponente> ObterPorParametros(AcaoComponenteParameters parameters)
        {
            var acaoComponente = _context.AcaoComponente
                .Include(i => i.Acao).Where(ac => ac.Acao.IndAtivo == 1)
                .Include(i => i.Causa).Where(ca => ca.Causa.IndAtivo == 1)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                acaoComponente = acaoComponente.Where(
                    c =>
                    c.Acao.NomeAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.Acao.CodEAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodAcaoComponente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodECausa))
            {
                acaoComponente = acaoComponente.Where(w => w.CodECausa == parameters.CodECausa);
            }

            return PagedList<AcaoComponente>.ToPagedList(acaoComponente, parameters.PageNumber, parameters.PageSize);
        }
    }
}
