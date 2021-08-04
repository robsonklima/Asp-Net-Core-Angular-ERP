using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IEquipamentoRepository
    {
        PagedList<Equipamento> ObterPorParametros(EquipamentoParameters parameters);
        Equipamento ObterPorCodigo(int codigo);
    }
}
