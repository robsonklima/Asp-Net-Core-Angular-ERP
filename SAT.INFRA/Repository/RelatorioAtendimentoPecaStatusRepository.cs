using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class RelatorioAtendimentoPecaStatusRepository : IRelatorioAtendimentoPecaStatusRepository
    {
        private readonly AppDbContext _context;

        public RelatorioAtendimentoPecaStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _context.ChangeTracker.Clear();
            RelatorioAtendimentoPecaStatus rps = _context.RelatorioAtendimentoPecaStatus
                .FirstOrDefault(rps => rps.CodRatpecasStatus == relatorioAtendimentoPecaStatus.CodRatpecasStatus);
            try
            {
                if (rps != null)
                {
                    _context.Entry(rps).CurrentValues.SetValues(relatorioAtendimentoPecaStatus);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            try
            {
                _context.Add(relatorioAtendimentoPecaStatus);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int CodRatpecasStatus)
        {
            RelatorioAtendimentoPecaStatus rps = _context.RelatorioAtendimentoPecaStatus
                .FirstOrDefault(rps => rps.CodRatpecasStatus == CodRatpecasStatus);

            if (rps != null)
            {
                _context.RelatorioAtendimentoPecaStatus.Remove(rps);
                _context.SaveChanges();
            }
        }

        public RelatorioAtendimentoPecaStatus ObterPorCodigo(int CodRatpecasStatus)
        {
            try
            {
                return _context.RelatorioAtendimentoPecaStatus
                    .SingleOrDefault(rps => rps.CodRatpecasStatus == CodRatpecasStatus);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a relatorioAtendimentoPecaStatus {ex.Message}");
            }
        }

        public PagedList<RelatorioAtendimentoPecaStatus> ObterPorParametros(RelatorioAtendimentoPecaStatusParameters parameters)
        {
            var relatorioAtendimentoPecaStatuss = _context.RelatorioAtendimentoPecaStatus
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                relatorioAtendimentoPecaStatuss = relatorioAtendimentoPecaStatuss.Where(a =>
                    a.CodRatpecasStatus.ToString().Contains(parameters.Filter));
            }

            if (parameters.CodRatpecasStatus != null)
            {
                relatorioAtendimentoPecaStatuss = relatorioAtendimentoPecaStatuss.Where(a => a.CodRatpecasStatus == parameters.CodRatpecasStatus);
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                relatorioAtendimentoPecaStatuss = relatorioAtendimentoPecaStatuss.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<RelatorioAtendimentoPecaStatus>.ToPagedList(relatorioAtendimentoPecaStatuss, parameters.PageNumber, parameters.PageSize);
        }

    }
}
