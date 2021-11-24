using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoEquipamentoDataService
    {
        ListViewModel ObterPorParametros(ContratoEquipamentoDataParameters parameters);
    }
}
