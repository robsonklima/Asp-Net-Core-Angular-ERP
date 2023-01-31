using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPecasLaboratorioService
    {
        ListViewModel ObterPorParametros(PecasLaboratorioParameters parameters);
    }
}