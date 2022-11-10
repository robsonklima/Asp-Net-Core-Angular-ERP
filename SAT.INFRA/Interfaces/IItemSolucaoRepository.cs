using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IItemSolucaoRepository
    {
        PagedList<ItemSolucao> ObterPorParametros(ItemSolucaoParameters parameters);
        ItemSolucao Criar(ItemSolucao itemSolucao);
        void Atualizar(ItemSolucao itemSolucao);
        void Deletar(int codigo);
        ItemSolucao ObterPorCodigo(int codigo);
    }
}
