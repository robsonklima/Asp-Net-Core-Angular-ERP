using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AdendoItemService : IAdendoItemService
    {
        private readonly IAdendoItemRepository _AdendoItemRepo;

        public AdendoItemService(IAdendoItemRepository AdendoItemRepo)
        {
            _AdendoItemRepo = AdendoItemRepo;
        }

        public AdendoItem Atualizar(AdendoItem item)
        {
            return _AdendoItemRepo.Atualizar(item);
        }

        public AdendoItem Criar(AdendoItem item)
        {
            return _AdendoItemRepo.Criar(item);
        }

        public AdendoItem Deletar(int codigo)
        {
            return _AdendoItemRepo.Deletar(codigo);
        }

        public AdendoItem ObterPorCodigo(int codigo)
        {
            return _AdendoItemRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AdendoItemParameters parameters)
        {
            var data = _AdendoItemRepo.ObterPorParametros(parameters);

            var model = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return model;
        }
    }
}
