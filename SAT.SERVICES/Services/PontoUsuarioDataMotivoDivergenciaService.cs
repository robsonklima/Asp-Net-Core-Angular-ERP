using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioDataMotivoDivergenciaService : IPontoUsuarioDataMotivoDivergenciaService
    {
        private readonly IPontoUsuarioDataMotivoDivergenciaRepository _pontoUsuarioDataMotivoDivergenciaRepo;

        public PontoUsuarioDataMotivoDivergenciaService(IPontoUsuarioDataMotivoDivergenciaRepository pontoUsuarioDataMotivoDivergenciaRepo)
        {
            _pontoUsuarioDataMotivoDivergenciaRepo = pontoUsuarioDataMotivoDivergenciaRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataMotivoDivergenciaParameters parameters)
        {
            var data = _pontoUsuarioDataMotivoDivergenciaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }
    }
}
