using SAT.INFRA.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.SERVICES.Services
{
    public class MonitoramentoService : IMonitoramentoService
    {
        private readonly IMonitoramentoRepository _monitoramentoRepository;

        public MonitoramentoService(IMonitoramentoRepository monitoramentoRepository)
        {
            this._monitoramentoRepository = monitoramentoRepository;
        }
        public MonitoramentoViewModel ObterListaMonitoramento()
        {
            return this._monitoramentoRepository.ObterListaMonitoramento();
        }
    }
}
