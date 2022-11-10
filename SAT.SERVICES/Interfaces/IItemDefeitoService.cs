using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IItemDefeitoService
    {
        ListViewModel ObterPorParametros(ItemDefeitoParameters parameters);
        ItemDefeito Criar(ItemDefeito itemDefeito);
        void Deletar(int codigo);
        void Atualizar(ItemDefeito itemDefeito);
        ItemDefeito ObterPorCodigo(int codigo);
    }
}
