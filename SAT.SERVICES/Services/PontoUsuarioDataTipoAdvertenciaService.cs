using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoUsuarioDataTipoAdvertenciaService : IPontoUsuarioDataTipoAdvertenciaService
    {
        private readonly IPontoUsuarioDataTipoAdvertenciaRepository _pontoUsuarioDataTipoAdvertenciaRepo;

        public PontoUsuarioDataTipoAdvertenciaService(IPontoUsuarioDataTipoAdvertenciaRepository pontoUsuarioDataTipoAdvertenciaRepo)
        {
            _pontoUsuarioDataTipoAdvertenciaRepo = pontoUsuarioDataTipoAdvertenciaRepo;
        }

        public ListViewModel ObterPorParametros(PontoUsuarioDataTipoAdvertenciaParameters parameters)
        {
            var data = _pontoUsuarioDataTipoAdvertenciaRepo.ObterPorParametros(parameters);

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
