using Microsoft.EntityFrameworkCore;
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
    public class RelatorioAtendimentoDetalhePecaStatusRepository : IRelatorioAtendimentoDetalhePecaStatusRepository
    {
        private readonly AppDbContext _context;

        public RelatorioAtendimentoDetalhePecaStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            _context.ChangeTracker.Clear();
            RelatorioAtendimentoDetalhePecaStatus rdp = _context.RelatorioAtendimentoDetalhePecaStatus
                .FirstOrDefault(rdp => rdp.CodRATDetalhesPecasStatus == relatorioAtendimentoDetalhePecaStatus.CodRATDetalhesPecasStatus);
            try
            {
                if (rdp != null)
                {
                    _context.Entry(rdp).CurrentValues.SetValues(relatorioAtendimentoDetalhePecaStatus);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            try
            {
                _context.Add(relatorioAtendimentoDetalhePecaStatus);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codRATDetalhesPecasStatus)
        {
            RelatorioAtendimentoDetalhePecaStatus rdp = _context.RelatorioAtendimentoDetalhePecaStatus
                .FirstOrDefault(aud => aud.CodRATDetalhesPecasStatus == codRATDetalhesPecasStatus);

            if (rdp != null)
            {
                _context.RelatorioAtendimentoDetalhePecaStatus.Remove(rdp);
                _context.SaveChanges();
            }
        }

        public RelatorioAtendimentoDetalhePecaStatus ObterPorCodigo(int codRATDetalhesPecasStatus)
        {
            try
            {
                return _context.RelatorioAtendimentoDetalhePecaStatus
                    .Include(c => c.Usuario)
                        .ThenInclude(c => c.Filial)
                    .Include(c => c.RelatorioAtendimentoPecaStatus)
                    .SingleOrDefault(aud => aud.CodRATDetalhesPecasStatus == codRATDetalhesPecasStatus);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a relatorioAtendimentoDetalhePecaStatus {ex.Message}");
            }
        }

        public PagedList<RelatorioAtendimentoDetalhePecaStatus> ObterPorParametros(RelatorioAtendimentoDetalhePecaStatusParameters parameters)
        {
            var relatorioAtendimentoDetalhePecaStatuss = _context.RelatorioAtendimentoDetalhePecaStatus
                    .Include(c => c.Usuario)
                        .ThenInclude(c => c.Filial)
                    .Include(c => c.RelatorioAtendimentoPecaStatus)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                relatorioAtendimentoDetalhePecaStatuss = relatorioAtendimentoDetalhePecaStatuss.Where(a =>
                    a.CodRATDetalhesPecasStatus.ToString().Contains(parameters.Filter) ||
                    a.Usuario.NomeUsuario.Contains(parameters.Filter));
            }

            if (parameters.CodRATDetalhesPecasStatus != null)
            {
                relatorioAtendimentoDetalhePecaStatuss = relatorioAtendimentoDetalhePecaStatuss.Where(a => a.CodRATDetalhesPecasStatus == parameters.CodRATDetalhesPecasStatus);
            };

            if (parameters.CodRATDetalhesPecas != null)
            {
                relatorioAtendimentoDetalhePecaStatuss = relatorioAtendimentoDetalhePecaStatuss.Where(a => a.CodRATDetalhesPecas == parameters.CodRATDetalhesPecas);
            };

            if (!string.IsNullOrWhiteSpace(parameters.CodRATDetalhesPecasIN))
            {
                int[] cods = parameters.CodRATDetalhesPecasIN.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                relatorioAtendimentoDetalhePecaStatuss = relatorioAtendimentoDetalhePecaStatuss.Where(dc => cods.Contains(dc.CodRATDetalhesPecas));
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                relatorioAtendimentoDetalhePecaStatuss = relatorioAtendimentoDetalhePecaStatuss.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<RelatorioAtendimentoDetalhePecaStatus>.ToPagedList(relatorioAtendimentoDetalhePecaStatuss, parameters.PageNumber, parameters.PageSize);
        }

    }
}
