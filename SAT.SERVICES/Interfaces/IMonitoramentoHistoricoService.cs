using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMonitoramentoHistoricoService
    {
        ListViewModel ObterPorParametros(MonitoramentoParameters parameters);
    }
}