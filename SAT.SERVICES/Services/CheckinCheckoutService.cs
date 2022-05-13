using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CheckinCheckoutService : ICheckinCheckoutService
    {
        private readonly ICheckinCheckoutRepository _checkinCheckoutRepo;

        public CheckinCheckoutService(ICheckinCheckoutRepository checkinCheckoutRepo)
        {
            _checkinCheckoutRepo = checkinCheckoutRepo;
        }

        public ListViewModel ObterPorParametros(CheckinCheckoutParameters parameters)
        {
            var checkinCheckouts = _checkinCheckoutRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = checkinCheckouts,
                TotalCount = checkinCheckouts.TotalCount,
                CurrentPage = checkinCheckouts.CurrentPage,
                PageSize = checkinCheckouts.PageSize,
                TotalPages = checkinCheckouts.TotalPages,
                HasNext = checkinCheckouts.HasNext,
                HasPrevious = checkinCheckouts.HasPrevious
            };

            return lista;
        }
    }
}
