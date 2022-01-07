using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
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
            var MonitoramentoHistoricos = _MonitoramentoHistoricoRepository.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = MonitoramentoHistoricos,
                TotalCount = MonitoramentoHistoricos.TotalCount,
                CurrentPage = MonitoramentoHistoricos.CurrentPage,
                PageSize = MonitoramentoHistoricos.PageSize,
                TotalPages = MonitoramentoHistoricos.TotalPages,
                HasNext = MonitoramentoHistoricos.HasNext,
                HasPrevious = MonitoramentoHistoricos.HasPrevious
            };
        }
    }
}