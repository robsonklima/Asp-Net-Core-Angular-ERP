using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ConferenciaParticipanteService : IConferenciaParticipanteService
    {
        private readonly IConferenciaParticipanteRepository _conferenciaParticipanteRepo;

        public ConferenciaParticipanteService(IConferenciaParticipanteRepository conferenciaParticipanteRepo)
        {
            _conferenciaParticipanteRepo = conferenciaParticipanteRepo;
        }

        public ListViewModel ObterPorParametros(ConferenciaParticipanteParameters parameters)
        {
            var conferencias = _conferenciaParticipanteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = conferencias,
                TotalCount = conferencias.TotalCount,
                CurrentPage = conferencias.CurrentPage,
                PageSize = conferencias.PageSize,
                TotalPages = conferencias.TotalPages,
                HasNext = conferencias.HasNext,
                HasPrevious = conferencias.HasPrevious
            };

            return lista;
        }

        public ConferenciaParticipante Criar(ConferenciaParticipante participante)
        {
            _conferenciaParticipanteRepo.Criar(participante);
            return participante;
        }

        public void Deletar(int codigo)
        {
            _conferenciaParticipanteRepo.Deletar(codigo);
        }

        public ConferenciaParticipante ObterPorCodigo(int codigo)
        {
            return _conferenciaParticipanteRepo.ObterPorCodigo(codigo);
        }
    }
}
