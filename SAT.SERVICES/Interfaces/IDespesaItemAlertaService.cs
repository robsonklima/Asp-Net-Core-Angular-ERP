using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaItemAlertaService
    {
        ListViewModel ObterPorParametros(DespesaItemAlertaParameters parameters);
    }
}