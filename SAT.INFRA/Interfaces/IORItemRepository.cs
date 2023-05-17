using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORItemRepository
    {
        PagedList<ORItem> ObterPorParametros(ORItemParameters parameters);
        void Criar(ORItem item);
        ORItem Atualizar(ORItem item);
        void Deletar(int codigo);
        ORItem ObterPorCodigo(int codigo);
    }
}
