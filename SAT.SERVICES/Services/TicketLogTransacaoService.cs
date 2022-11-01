using SAT.INFRA.Interfaces;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Services
{
    public class TicketLogTransacaoService : ITicketLogTransacaoService
    {
        private readonly ITicketLogTransacaoRepository _TicketLogTransacaoRepo;

        public TicketLogTransacaoService(
            ITicketLogTransacaoRepository TicketLogTransacaoRepo)
        {
            _TicketLogTransacaoRepo = TicketLogTransacaoRepo;
        }

        public ListViewModel ObterPorParametros(TicketLogTransacaoParameters parameters)
        {
            var tickets = _TicketLogTransacaoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = tickets,
                TotalCount = tickets.TotalCount,
                CurrentPage = tickets.CurrentPage,
                PageSize = tickets.PageSize,
                TotalPages = tickets.TotalPages,
                HasNext = tickets.HasNext,
                HasPrevious = tickets.HasPrevious
            };
        }
    }
}