using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoEquipamentoService
    {
        ListViewModel ObterPorParametros(ContratoEquipamentoParameters parameters);
    }
}
