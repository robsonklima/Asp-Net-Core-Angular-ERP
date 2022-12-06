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
    public class LaudoStatusRepository : ILaudoStatusRepository
    {
        private readonly AppDbContext _context;

        public LaudoStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public LaudoStatus ObterPorCodigo(int codigo)
        {
            try
            {
                return _context.LaudoStatus
                .SingleOrDefault(a => a.CodLaudoStatus == codigo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a laudo Status {ex.Message}");
            }
        }

        public PagedList<LaudoStatus> ObterPorParametros(LaudoStatusParameters parameters)
        {
            var laudos = _context.LaudoStatus
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                laudos = laudos.Where(a =>
                    a.CodLaudoStatus.ToString().Contains(parameters.Filter));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                laudos = laudos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<LaudoStatus>.ToPagedList(laudos, parameters.PageNumber, parameters.PageSize);
        }

    }
}