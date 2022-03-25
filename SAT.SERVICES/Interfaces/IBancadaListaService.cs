using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IBancadaListaService
    {
        ListViewModel ObterPorParametros(BancadaListaParameters parameters);
    }
}
