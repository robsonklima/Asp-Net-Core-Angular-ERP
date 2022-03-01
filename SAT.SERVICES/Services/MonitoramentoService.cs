using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MonitoramentoService : IMonitoramentoService
    {
        private readonly IMonitoramentoRepository _monitoramentoRepository;

        public MonitoramentoService(IMonitoramentoRepository monitoramentoRepository)
        {
            this._monitoramentoRepository = monitoramentoRepository;
        }
        
        public ListViewModel ObterPorParametros(MonitoramentoParameters parameters)
        {
            var monitoramentos = _monitoramentoRepository.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = monitoramentos,
                TotalCount = monitoramentos.TotalCount,
                CurrentPage = monitoramentos.CurrentPage,
                PageSize = monitoramentos.PageSize,
                TotalPages = monitoramentos.TotalPages,
                HasNext = monitoramentos.HasNext,
                HasPrevious = monitoramentos.HasPrevious
            };
        }
    }
}