using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IGrupoEquipamentoRepository
    {
        PagedList<GrupoEquipamento> ObterPorParametros(GrupoEquipamentoParameters parameters);
        void Criar(GrupoEquipamento grupoEquipamento);
        void Deletar(int codigo);
        void Atualizar(GrupoEquipamento grupoEquipamento);
        GrupoEquipamento ObterPorCodigo(int codGrupoEquip, int codTipoEquip);
    }
}
