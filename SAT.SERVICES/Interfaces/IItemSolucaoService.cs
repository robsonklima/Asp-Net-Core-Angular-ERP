using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IItemSolucaoService
    {
        ListViewModel ObterPorParametros(ItemSolucaoParameters parameters);
        ItemSolucao Criar(ItemSolucao itemSolucao);
        void Deletar(int codigo);
        void Atualizar(ItemSolucao itemSolucao);
        ItemSolucao ObterPorCodigo(int codigo);
    }
}
