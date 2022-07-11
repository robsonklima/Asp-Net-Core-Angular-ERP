using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISatTaskRepository
    {
        PagedList<SatTask> ObterPorParametros(SatTaskParameters parameters);
        void Criar(SatTask task);
        void Deletar(int codigo);
        void Atualizar(SatTask task);
        SatTask ObterPorCodigo(int codigo);
    }
}
