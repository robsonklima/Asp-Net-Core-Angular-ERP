using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ISatTaskService
    {
        ListViewModel ObterPorParametros(SatTaskParameters parameters);
        SatTask Criar(SatTask SatTask);
        SatTask Deletar(int codigo);
        SatTask Atualizar(SatTask SatTask);
        SatTask ObterPorCodigo(int codigo);
    }
}
