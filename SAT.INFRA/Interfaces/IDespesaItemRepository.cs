using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaItemRepository
    {
        PagedList<DespesaItem> ObterPorParametros(DespesaItemParameters parameters);
        void Criar(DespesaItem despesaItem);
        void Deletar(int codigo);
        void Atualizar(DespesaItem despesaItem);
        DespesaItem ObterPorCodigo(int codigo);
    }
}