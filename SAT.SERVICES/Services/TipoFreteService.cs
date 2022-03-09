using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoFreteService : ITipoFreteService
    {
        private readonly ITipoFreteRepository _tipoFreteRepo;

        public TipoFreteService(ITipoFreteRepository tipoFreteRepo)
        {
            _tipoFreteRepo = tipoFreteRepo;
        }

        public ListViewModel ObterPorParametros(TipoFreteParameters parameters)
        {
            var tipoFretes = _tipoFreteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipoFretes,
                TotalCount = tipoFretes.TotalCount,
                CurrentPage = tipoFretes.CurrentPage,
                PageSize = tipoFretes.PageSize,
                TotalPages = tipoFretes.TotalPages,
                HasNext = tipoFretes.HasNext,
                HasPrevious = tipoFretes.HasPrevious
            };

            return lista;
        }
    }
}
