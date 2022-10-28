using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IORCheckListItemService
    {
        ListViewModel ObterPorParametros(ORCheckListItemParameters parameters);
        ORCheckListItem Criar(ORCheckListItem item);
        void Deletar(int codigo);
        void Atualizar(ORCheckListItem item);
        ORCheckListItem ObterPorCodigo(int codigo);
    }
}
