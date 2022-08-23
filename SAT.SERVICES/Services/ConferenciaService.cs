using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ConferenciaService : IConferenciaService
    {
        private readonly IConferenciaRepository _conferenciaRepo;

        public ConferenciaService(IConferenciaRepository conferenciaRepo)
        {
            _conferenciaRepo = conferenciaRepo;
        }

        public ListViewModel ObterPorParametros(ConferenciaParameters parameters)
        {
            var conferencias = _conferenciaRepo.ObterPorParametros(parameters);

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

        public Conferencia Criar(Conferencia Conferencia)
        {
            _conferenciaRepo.Criar(Conferencia);
            return Conferencia;
        }

        public void Deletar(int codigo)
        {
            _conferenciaRepo.Deletar(codigo);
        }

        public Conferencia ObterPorCodigo(int codigo)
        {
            return _conferenciaRepo.ObterPorCodigo(codigo);
        }
    }
}
