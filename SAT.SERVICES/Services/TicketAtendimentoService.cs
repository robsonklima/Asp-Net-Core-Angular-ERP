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

        // public Ticket Criar(Ticket ticket)
        // {
        //     // _ticketRepo.Criar(ticket);
        //     return null;
        //     // return ticket;
        // }

        // public void Deletar(int codigo)
        // {
        //     return null;
        //     // _ticketRepo.Deletar(codigo);
        // }


        // public void Atualizar(Ticket ticket)
        // {
        //     // var equip = _ticketRepo.ObterPorCodigo(ticket.);
        //     // ticket.CodTipoEquip = equip.CodTipoEquip;
        //     // ticket.CodGrupoEquip = equip.CodGrupoEquip;
        //     // _equipamentoContratoRepo.Atualizar(equipamentoContrato);
        //     return null;
        // }

        // public Ticket Criar(EquipamentoContrato equipamentoContrato)
        // {
        //     throw new System.NotImplementedException();
        // }


    }
}
