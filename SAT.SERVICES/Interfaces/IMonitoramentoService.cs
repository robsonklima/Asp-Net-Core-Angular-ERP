using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMonitoramentoService
    {
        ListViewModel ObterPorParametros(MonitoramentoParameters parameters);
    }
}