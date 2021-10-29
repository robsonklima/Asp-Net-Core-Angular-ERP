using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaAdiantamentoRepository
    {
        PagedList<DespesaAdiantamento> ObterPorParametros(DespesaAdiantamentoParameters parameters);
        void Criar(DespesaAdiantamento despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaAdiantamento despesa);
        DespesaAdiantamento ObterPorCodigo(int codigo);
    }
}