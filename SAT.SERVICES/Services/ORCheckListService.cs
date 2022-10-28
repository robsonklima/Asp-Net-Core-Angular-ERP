using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORCheckListService : IORCheckListService
    {
        private readonly IORCheckListRepository _orCheckListRepo;

        public ORCheckListService(IORCheckListRepository orCheckListRepo)
        {
            _orCheckListRepo = orCheckListRepo;
        }

        public ListViewModel ObterPorParametros(ORCheckListParameters parameters)
        {
            var checkLists = _orCheckListRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = checkLists,
                TotalCount = checkLists.TotalCount,
                CurrentPage = checkLists.CurrentPage,
                PageSize = checkLists.PageSize,
                TotalPages = checkLists.TotalPages,
                HasNext = checkLists.HasNext,
                HasPrevious = checkLists.HasPrevious
            };

            return lista;
        }

        public ORCheckList Criar(ORCheckList checkList)
        {
            _orCheckListRepo.Criar(checkList);

            return checkList;
        }

        public void Deletar(int codigo)
        {
            _orCheckListRepo.Deletar(codigo);
        }

        public void Atualizar(ORCheckList checkList)
        {
            _orCheckListRepo.Atualizar(checkList);
        }

        public ORCheckList ObterPorCodigo(int codigo)
        {
            return _orCheckListRepo.ObterPorCodigo(codigo);
        }
    }
}
