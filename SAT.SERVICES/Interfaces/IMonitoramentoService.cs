using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IMonitoramentoService
    {
        MonitoramentoCliente[] ObterPorParametros(MonitoramentoClienteParameters parameters);
    }
}