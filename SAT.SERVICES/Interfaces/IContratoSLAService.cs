using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoSLAService
    {
        ListViewModel ObterPorParametros(ContratoSLAParameters parameters);
    }
}
