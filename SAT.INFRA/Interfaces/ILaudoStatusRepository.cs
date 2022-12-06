using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILaudoStatusRepository
    {
        LaudoStatus ObterPorCodigo(int codigo);
        PagedList<LaudoStatus> ObterPorParametros(LaudoStatusParameters parameters);
    }
}
