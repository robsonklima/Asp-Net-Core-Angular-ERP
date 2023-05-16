using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IEquipamentoPOSRepository
    {
        PagedList<EquipamentoPOS> ObterPorParametros(EquipamentoPOSParameters parameters);
        EquipamentoPOS Criar(EquipamentoPOS registro);
        EquipamentoPOS Deletar(int codigo);
        EquipamentoPOS Atualizar(EquipamentoPOS registro);
        EquipamentoPOS ObterPorCodigo(int codigo);
    }
}
