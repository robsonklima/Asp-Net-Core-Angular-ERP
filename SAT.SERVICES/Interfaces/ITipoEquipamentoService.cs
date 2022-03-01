using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoEquipamentoService
    {
        ListViewModel ObterPorParametros(TipoEquipamentoParameters parameters);
        TipoEquipamento Criar(TipoEquipamento tipoEquipamento);
        void Deletar(int codigo);
        void Atualizar(TipoEquipamento tipoEquipamento);
        TipoEquipamento ObterPorCodigo(int codigo);
    }
}
