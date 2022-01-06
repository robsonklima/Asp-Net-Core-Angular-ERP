using System.Linq;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface ILogAlertaRepository
    {
        IQueryable<LogAlerta> ObterPorQuery(LogAlertaParameters parameters);
    }
}