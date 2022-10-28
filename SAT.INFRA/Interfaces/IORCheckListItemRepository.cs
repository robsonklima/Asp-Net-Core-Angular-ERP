using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORCheckListItemRepository
    {
        PagedList<ORCheckListItem> ObterPorParametros(ORCheckListItemParameters parameters);
        void Criar(ORCheckListItem item);
        void Atualizar(ORCheckListItem item);
        void Deletar(int cod);
        ORCheckListItem ObterPorCodigo(int cod);
    }
}
