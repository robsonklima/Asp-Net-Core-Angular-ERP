using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoSTNOrigemService
    {
        ListViewModel ObterPorParametros(OrdemServicoSTNOrigemParameters parameters);

        OrdemServicoSTNOrigem ObterPorCodigo(int codigo);
    }
}
