using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IItemXORCheckListService
    {
        ListViewModel ObterPorParametros(ItemXORCheckListParameters parameters);
        ItemXORCheckList Criar(ItemXORCheckList or);
        void Deletar(int codigo);
        void Atualizar(ItemXORCheckList or);
        ItemXORCheckList ObterPorCodigo(int codigo);
    }
}
