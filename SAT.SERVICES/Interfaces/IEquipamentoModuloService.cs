using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IEquipamentoModuloService
    {
        ListViewModel ObterPorParametros(EquipamentoModuloParameters  parameters);
        EquipamentoModulo Criar(EquipamentoModulo equipamentoModulo);
        void Deletar(int codigo);
        void Atualizar(EquipamentoModulo equipamentoModulo);
        EquipamentoModulo ObterPorCodigo(int codigo);
    }
}
