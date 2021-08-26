using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IGrupoEquipamentoServico
    {
        ListViewModel ObterPorParametros(GrupoEquipamentoParameters parameters);
        GrupoEquipamento Criar(GrupoEquipamento grupoEquipamento);
        void Deletar(int codigo);
        void Atualizar(GrupoEquipamento grupoEquipamento);
        GrupoEquipamento ObterPorCodigo(int codigo);
    }
}
