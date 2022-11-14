using System;
using System.IO;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TicketAnexoService : ITicketAnexoService 
    {
        private readonly ITicketAnexoRepository _ticketAnexoRepo;

        public TicketAnexoService(
            ITicketAnexoRepository ticketAnexoRepo
        )
        {
            _ticketAnexoRepo = ticketAnexoRepo;
        }

        public TicketAnexo ObterPorCodigo(int codigo)
        {
            return _ticketAnexoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(TicketAnexoParameters parameters)
        {
            var tickets = _ticketAnexoRepo.ObterPorParametros(parameters);

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

        public TicketAnexo Criar(TicketAnexo anexo)
        {
            return _ticketAnexoRepo.Criar(anexo);
        }

        public TicketAnexo Deletar(int codigo)
        {
            return _ticketAnexoRepo.Deletar(codigo);
        }

        public TicketAnexo Atualizar(TicketAnexo anexo)
        {
            return _ticketAnexoRepo.Atualizar(anexo);
        }
    }
}
