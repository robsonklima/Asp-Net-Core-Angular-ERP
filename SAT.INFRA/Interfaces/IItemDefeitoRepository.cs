using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IItemDefeitoRepository
    {
        PagedList<ItemDefeito> ObterPorParametros(ItemDefeitoParameters parameters);
        ItemDefeito Criar(ItemDefeito itemDefeito);
        void Atualizar(ItemDefeito itemDefeito);
        void Deletar(int codigo);
        ItemDefeito ObterPorCodigo(int codigo);
    }
}
