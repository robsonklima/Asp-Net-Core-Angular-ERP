using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class BancadaListaService : IBancadaListaService
    {
        private readonly IBancadaListaRepository _bancadaListaRepository;

        public BancadaListaService(IBancadaListaRepository bancadaListaRepository)
        {
            this._bancadaListaRepository = bancadaListaRepository;
        }

        public ListViewModel ObterPorParametros(BancadaListaParameters parameters)
        {
            var clienteBancadas = _bancadaListaRepository.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clienteBancadas,
                TotalCount = clienteBancadas.TotalCount,
                CurrentPage = clienteBancadas.CurrentPage,
                PageSize = clienteBancadas.PageSize,
                TotalPages = clienteBancadas.TotalPages,
                HasNext = clienteBancadas.HasNext,
                HasPrevious = clienteBancadas.HasPrevious
            };

            return lista;
        }
    }
}
