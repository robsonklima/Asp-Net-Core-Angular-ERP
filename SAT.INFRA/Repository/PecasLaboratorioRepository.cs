using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public partial class PecasLaboratorioRepository : IPecasLaboratorioRepository
    {
        private readonly AppDbContext _context;

        public PecasLaboratorioRepository(AppDbContext context)
        {
            _context = context;
        }
        public PagedList<PecasLaboratorio> ObterPorParametros(PecasLaboratorioParameters parameters)
        {
            var query = _context.PecasLaboratorio
                .Include(p => p.Peca)
                .Include(p => p.PecaStatus)
                .Include(p => p.PecaFamilia)
                .Include(p => p.ORCheckList)
                .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                query = query.Where(p =>
                    p.CodPeca.ToString().Contains(parameters.Filter) ||
                    p.CodMagnus.Contains(parameters.Filter) ||
                    p.NomePeca.Contains(parameters.Filter)
                );
            }

            if (!string.IsNullOrEmpty(parameters.CodPecas))
            {
                int[] split = parameters.CodPecas.Split(",").Select(s => int.Parse(s)).ToArray();
                query = query.Where(p => split.Any(s => s.Equals(p.CodPeca)));
            }

            if (!string.IsNullOrEmpty(parameters.CodMagnus))
            {
                query = query.Where(p => p.CodMagnus == parameters.CodMagnus);
            }

            if (parameters.CodChecklist.HasValue)
            {
                query = query.Where(p => p.ORCheckList.CodORCheckList == parameters.CodChecklist);
            }

            return PagedList<PecasLaboratorio>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}