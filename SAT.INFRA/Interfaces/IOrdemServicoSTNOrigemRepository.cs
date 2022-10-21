using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoSTNOrigemRepository
    {
        PagedList<OrdemServicoSTNOrigem> ObterPorParametros(OrdemServicoSTNOrigemParameters parameters);
        OrdemServicoSTNOrigem ObterPorCodigo(int codigo);
    }
}
