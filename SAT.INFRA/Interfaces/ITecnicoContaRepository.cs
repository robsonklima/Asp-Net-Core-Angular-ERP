using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface ITecnicoContaRepository
    {
        PagedList<TecnicoConta> ObterPorParametros(TecnicoContaParameters parameters);
        TecnicoConta Criar(TecnicoConta conta);
        void Atualizar(TecnicoConta conta);
        void Deletar(int codigo);
        TecnicoConta ObterPorCodigo(int codigo);
    }
}
