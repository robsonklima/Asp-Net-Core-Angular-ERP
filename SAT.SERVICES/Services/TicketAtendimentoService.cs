using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketAtendimentoService : ITicketAtendimentoService 
    {
        private readonly ITicketAtendimentoRepository _ticketAtendimentoRepo;

        public TicketAtendimentoService(
            ITicketAtendimentoRepository ticketAtendimentoRepo
        )
        {
            _ticketAtendimentoRepo = ticketAtendimentoRepo;
        }

        public TicketAtendimento ObterPorCodigo(int codigo)
        {
            return _ticketAtendimentoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketAtendimentoParameters parameters)
        {
            var tickets = _ticketAtendimentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tickets,
                TotalCount = tickets.TotalCount,
                CurrentPage = tickets.CurrentPage,
                PageSize = tickets.PageSize,
                TotalPages = tickets.TotalPages,
                HasNext = tickets.HasNext,
                HasPrevious = tickets.HasPrevious
            };

            return lista;
        }

        public TicketAtendimento Criar(TicketAtendimento atend)
        {
            _ticketAtendimentoRepo.Criar(atend);

            return atend;
        }

        public TicketAtendimento Deletar(int codigo)
        {
            return _ticketAtendimentoRepo.Deletar(codigo);
        }

        public TicketAtendimento Atualizar(TicketAtendimento atend)
        {
            return _ticketAtendimentoRepo.Atualizar(atend);
        }
    }
}
