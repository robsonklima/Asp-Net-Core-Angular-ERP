using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class MonitoramentoRepository : IMonitoramentoRepository
    {
        private readonly AppDbContext _context;

        public MonitoramentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<Monitoramento> ObterPorParametros(MonitoramentoParameters parameters)
        {
            var monitoramentos = _context.Monitoramento.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Tipo))
                monitoramentos = monitoramentos.Where(i => i.Tipo == parameters.Tipo);

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                monitoramentos = monitoramentos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Monitoramento>.ToPagedList(monitoramentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}