using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaAdiantamentoPeriodoRepository
    {
        PagedList<DespesaAdiantamentoPeriodo> ObterPorParametros(DespesaAdiantamentoPeriodoParameters parameters);
        void Criar(DespesaAdiantamentoPeriodo despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaAdiantamentoPeriodo despesa);
        DespesaAdiantamentoPeriodo ObterPorCodigo(int codigo);
    }
}