using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ILaudoStatusService
    {
        ListViewModel ObterPorParametros(LaudoStatusParameters parameters);
        LaudoStatus ObterPorCodigo(int codigo);
    }
}