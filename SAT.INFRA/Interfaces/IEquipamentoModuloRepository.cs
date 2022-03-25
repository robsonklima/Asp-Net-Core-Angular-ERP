using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IEquipamentoModuloRepository
    {
        PagedList<EquipamentoModulo> ObterPorParametros(EquipamentoModuloParameters parameters);
        void Criar(EquipamentoModulo equipamentoModulo);
        void Deletar(int codigo);
        void Atualizar(EquipamentoModulo equipamentoModulo);
        EquipamentoModulo ObterPorCodigo(int codigo);
        bool ExisteEquipamentoModulo(EquipamentoModulo equipamentoModulo);
    }
}
