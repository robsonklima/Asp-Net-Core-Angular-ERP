using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IMonitoramentoService
    {
        Monitoramento[] ObterPorParametros(MonitoramentoParameters parameters);
    }
}