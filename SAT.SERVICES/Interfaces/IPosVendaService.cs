using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPosVendaService
    {
        ListViewModel ObterPorParametros(PosVendaParameters parameters);
        PosVenda ObterPorCodigo(int codigo);
    }
}
