using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoStatusService : IOrcamentoStatusService
    {
        private readonly IOrcamentoStatusRepository _orcStatusRepo;

        public OrcamentoStatusService(IOrcamentoStatusRepository orcStatusRepo)
        {
            _orcStatusRepo = orcStatusRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoStatusParameters parameters)
        {
            var status = _orcStatusRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = status,
                TotalCount = status.TotalCount,
                CurrentPage = status.CurrentPage,
                PageSize = status.PageSize,
                TotalPages = status.TotalPages,
                HasNext = status.HasNext,
                HasPrevious = status.HasPrevious
            };
        }
    }
}