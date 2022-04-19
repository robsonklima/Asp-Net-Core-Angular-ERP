using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class MonitoramentoHistoricoRepository : IMonitoramentoHistoricoRepository
    {
        private readonly AppDbContext _context;

        public MonitoramentoHistoricoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<MonitoramentoHistorico> ObterPorParametros(MonitoramentoParameters parameters)
        {
            var query = _context.MonitoramentoHistorico.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Tipo))
                query = query.Where(i => i.Tipo == parameters.Tipo);

            if (!string.IsNullOrWhiteSpace(parameters.Servidor))
                query = query.Where(i => i.Servidor == parameters.Servidor);

            if (!string.IsNullOrWhiteSpace(parameters.Item))
                query = query.Where(i => i.Item == parameters.Item);

            if (parameters.DataHoraProcessamento.HasValue)
                query = query.Where(i =>
                    i.DataHoraProcessamento.HasValue && i.DataHoraProcessamento.Value.Date == parameters.DataHoraProcessamento.Value.Date);

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<MonitoramentoHistorico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}