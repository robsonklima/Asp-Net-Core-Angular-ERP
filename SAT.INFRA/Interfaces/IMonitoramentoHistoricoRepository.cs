using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMonitoramentoHistoricoRepository
    {
        PagedList<MonitoramentoHistorico> ObterPorParametros(MonitoramentoParameters parameters);
    }
}