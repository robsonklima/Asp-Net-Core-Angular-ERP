using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class MonitoramentoRepository : IMonitoramentoRepository
    {
        private readonly AppDbContext _context;

        public MonitoramentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Monitoramento> ObterPorQuery(MonitoramentoParameters parameters) =>
            _context.Monitoramento.AsQueryable();

        public PagedList<Monitoramento> ObterPorParametros(MonitoramentoParameters parameters)
        {
            var monitoramentos = _context.Monitoramento.AsQueryable();
            return PagedList<Monitoramento>.ToPagedList(monitoramentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}