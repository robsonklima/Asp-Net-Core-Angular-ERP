using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class LogAlertaRepository : ILogAlertaRepository
    {
        private readonly AppDbContext _context;

        public LogAlertaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<LogAlerta> ObterPorQuery(LogAlertaParameters parameters)
        {
            var alertas = _context.LogAlerta.AsQueryable();
            return alertas;
        }
    }
}