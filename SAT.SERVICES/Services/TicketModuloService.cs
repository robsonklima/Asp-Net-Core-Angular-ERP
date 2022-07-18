using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketModuloService : ITicketModuloService
    {
        private readonly ITicketModuloRepository _ticketModuloRepo;

        public TicketModuloService(
            ITicketModuloRepository ticketModuloRepo
        )
        {
            _ticketModuloRepo = ticketModuloRepo;
        }
        
        public TicketModulo ObterPorCodigo(int codigo)
        {
            return _ticketModuloRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketModuloParameters parameters)
        {
            var ticketModulos = _ticketModuloRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ticketModulos,
                TotalCount = ticketModulos.TotalCount,
                CurrentPage = ticketModulos.CurrentPage,
                PageSize = ticketModulos.PageSize,
                TotalPages = ticketModulos.TotalPages,
                HasNext = ticketModulos.HasNext,
                HasPrevious = ticketModulos.HasPrevious
            };

            return lista;
        }

     

        // public TicketModulo Criar(TicketModulo ticketModulo)
        // {
        //     _ticketModuloRepo.Criar(TicketModulo);
        //     return null;
        //     return TicketModulo;
        // }

        // public void Deletar(int codigo)
        // {
        //     return null;
        //     _ticketModuloRepo.Deletar(codigo);
        // }


        public TicketModulo Atualizar(TicketModulo ticketModulo)
        {

            _ticketModuloRepo.Atualizar(ticketModulo);
            return ticketModulo;
        }

        //  public TicketModulo Criar(TicketModulo ticketModulo)
        //  {
        //      throw new System.NotImplementedException();
        //  }


    }
}
