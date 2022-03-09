using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IMoedaService
    {
        ListViewModel ObterPorParametros(MoedaParameters parameters);
    }
}
