using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaRepository
    {
        PagedList<Despesa> ObterPorParametros(DespesaParameters parameters);
        Despesa Criar(Despesa despesa);
        void Deletar(int codigo);
        void Atualizar(Despesa despesa);
        Despesa ObterPorCodigo(int codigo);
    }
}