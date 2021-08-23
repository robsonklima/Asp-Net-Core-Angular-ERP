using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IContratoEquipamentoRepository
    {
        PagedList<ContratoEquipamento> ObterPorParametros(ContratoEquipamentoParameters parameters);
    }
}
