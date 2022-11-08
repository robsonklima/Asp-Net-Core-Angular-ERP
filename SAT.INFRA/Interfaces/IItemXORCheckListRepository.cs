using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IItemXORCheckListRepository
    {
        PagedList<ItemXORCheckList> ObterPorParametros(ItemXORCheckListParameters parameters);
        void Criar(ItemXORCheckList item);
        void Atualizar(ItemXORCheckList item);
        void Deletar(int cod);
        ItemXORCheckList ObterPorCodigo(int cod);
    }
}
