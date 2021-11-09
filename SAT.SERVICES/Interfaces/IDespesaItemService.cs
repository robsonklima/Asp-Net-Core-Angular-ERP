using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaItemService
    {
        ListViewModel ObterPorParametros(DespesaItemParameters parameters);
        DespesaItem Criar(DespesaItem despesaItem);
        void Deletar(int codigo);
        void Atualizar(DespesaItem despesaItem);
        DespesaItem ObterPorCodigo(int codigo);
    }
}