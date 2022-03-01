using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MonitoramentoHistoricoService : IMonitoramentoHistoricoService
    {
        private readonly IMonitoramentoHistoricoRepository _MonitoramentoHistoricoRepository;

        public MonitoramentoHistoricoService(IMonitoramentoHistoricoRepository MonitoramentoHistoricoRepository)
        {
            this._MonitoramentoHistoricoRepository = MonitoramentoHistoricoRepository;
        }

        public ListViewModel ObterPorParametros(MonitoramentoParameters parameters)
        {
            var historicos = _MonitoramentoHistoricoRepository.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = historicos,
                TotalCount = historicos.TotalCount,
                CurrentPage = historicos.CurrentPage,
                PageSize = historicos.PageSize,
                TotalPages = historicos.TotalPages,
                HasNext = historicos.HasNext,
                HasPrevious = historicos.HasPrevious
            };
        }
    }
}