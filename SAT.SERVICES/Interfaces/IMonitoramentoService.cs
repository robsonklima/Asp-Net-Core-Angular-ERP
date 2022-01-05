using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IMonitoramentoService
    {
        List<MonitoramentoClienteViewModel> ObterListaMonitoramentoClientes();
    }
}
