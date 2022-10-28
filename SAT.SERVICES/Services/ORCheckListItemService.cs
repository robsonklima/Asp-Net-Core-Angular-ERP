using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORCheckListItemService : IORCheckListItemService
    {
        private readonly IORCheckListItemRepository _ORCheckListItemRepo;

        public ORCheckListItemService(IORCheckListItemRepository ORCheckListItemRepo)
        {
            _ORCheckListItemRepo = ORCheckListItemRepo;
        }

        public ListViewModel ObterPorParametros(ORCheckListItemParameters parameters)
        {
            var itens = _ORCheckListItemRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = itens,
                TotalCount = itens.TotalCount,
                CurrentPage = itens.CurrentPage,
                PageSize = itens.PageSize,
                TotalPages = itens.TotalPages,
                HasNext = itens.HasNext,
                HasPrevious = itens.HasPrevious
            };

            return lista;
        }

        public ORCheckListItem Criar(ORCheckListItem item)
        {
            _ORCheckListItemRepo.Criar(item);

            return item;
        }

        public void Deletar(int codigo)
        {
            _ORCheckListItemRepo.Deletar(codigo);
        }

        public void Atualizar(ORCheckListItem item)
        {
            _ORCheckListItemRepo.Atualizar(item);
        }

        public ORCheckListItem ObterPorCodigo(int codigo)
        {
            return _ORCheckListItemRepo.ObterPorCodigo(codigo);
        }
    }
}
