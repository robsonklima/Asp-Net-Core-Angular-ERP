using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PecaListaService : IPecaListaService
    {
        private readonly IPecaListaRepository _pecaListaRepo;

        public PecaListaService(IPecaListaRepository pecaListaRepo)
        {
            _pecaListaRepo = pecaListaRepo;
        }

        public ListViewModel ObterPorParametros(PecaListaParameters parameters)
        {
            var pecasLista = _pecaListaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pecasLista,
                TotalCount = pecasLista.TotalCount,
                CurrentPage = pecasLista.CurrentPage,
                PageSize = pecasLista.PageSize,
                TotalPages = pecasLista.TotalPages,
                HasNext = pecasLista.HasNext,
                HasPrevious = pecasLista.HasPrevious
            };

            return lista;
        }
    }
}
