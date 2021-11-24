using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaItemAlertaService : IDespesaItemAlertaService
    {
        private readonly IDespesaItemAlertaRepository _alertaRepo;

        public DespesaItemAlertaService(IDespesaItemAlertaRepository alertaRepo)
        {
            _alertaRepo = alertaRepo;
        }

        public ListViewModel ObterPorParametros(DespesaItemAlertaParameters parameters)
        {
            var alertas =
                _alertaRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = alertas,
                TotalCount = alertas.TotalCount,
                CurrentPage = alertas.CurrentPage,
                PageSize = alertas.PageSize,
                TotalPages = alertas.TotalPages,
                HasNext = alertas.HasNext,
                HasPrevious = alertas.HasPrevious
            };
        }
    }
}