using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISatTaskRepository
    {
        PagedList<SatTask> ObterPorParametros(SatTaskParameters parameters);
        SatTask Criar(SatTask task);
        SatTask Deletar(int codigo);
        SatTask Atualizar(SatTask task);
        SatTask ObterPorCodigo(int codigo);
    }
}
