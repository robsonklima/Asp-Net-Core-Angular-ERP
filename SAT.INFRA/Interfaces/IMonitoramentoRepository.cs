using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IMonitoramentoRepository
    {
        MonitoramentoViewModel ObterListaMonitoramento();
    }
}
