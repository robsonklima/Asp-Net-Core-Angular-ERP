using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoEquipamentoRepository
    {
        PagedList<ContratoEquipamento> ObterPorParametros(ContratoEquipamentoParameters parameters);
    }
}
