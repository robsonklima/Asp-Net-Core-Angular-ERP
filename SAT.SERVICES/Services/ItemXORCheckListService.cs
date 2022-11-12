using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ItemXORCheckListService : IItemXORCheckListService
    {
        private readonly IItemXORCheckListRepository _ItemXORCheckListRepo;

        public ItemXORCheckListService(
            IItemXORCheckListRepository ItemXORCheckListRepo
        )
        {
            _ItemXORCheckListRepo = ItemXORCheckListRepo;
        }

        public ListViewModel ObterPorParametros(ItemXORCheckListParameters parameters)
        {
            var ItemXORCheckLists = _ItemXORCheckListRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ItemXORCheckLists,
                TotalCount = ItemXORCheckLists.TotalCount,
                CurrentPage = ItemXORCheckLists.CurrentPage,
                PageSize = ItemXORCheckLists.PageSize,
                TotalPages = ItemXORCheckLists.TotalPages,
                HasNext = ItemXORCheckLists.HasNext,
                HasPrevious = ItemXORCheckLists.HasPrevious
            };

            return lista;
        }

        public ItemXORCheckList Criar(ItemXORCheckList itemXORCheckList)
        {
            _ItemXORCheckListRepo.Criar(itemXORCheckList);

            return itemXORCheckList;
        }

        public void Deletar(int codigo)
        {
            _ItemXORCheckListRepo.Deletar(codigo);
        }

        public void Atualizar(ItemXORCheckList itemXORCheckList)
        {
            _ItemXORCheckListRepo.Atualizar(itemXORCheckList);
        }

        public ItemXORCheckList ObterPorCodigo(int codigo)
        {
            return _ItemXORCheckListRepo.ObterPorCodigo(codigo);
        }
    }
}
