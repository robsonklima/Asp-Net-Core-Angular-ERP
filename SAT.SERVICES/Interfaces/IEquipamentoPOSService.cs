using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IEquipamentoPOSService
    {
        ListViewModel ObterPorParametros(EquipamentoPOSParameters parameters);
        EquipamentoPOS Criar(EquipamentoPOS r);
        EquipamentoPOS Deletar(int codigo);
        EquipamentoPOS Atualizar(EquipamentoPOS r);
        EquipamentoPOS ObterPorCodigo(int codigo);
    }
}
