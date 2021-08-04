using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.API.Repositories.Interfaces
{
    public interface IGrupoEquipamentoRepository
    {
        PagedList<GrupoEquipamento> ObterPorParametros(GrupoEquipamentoParameters parameters);
        void Criar(GrupoEquipamento grupoEquipamento);
        void Deletar(int codigo);
        void Atualizar(GrupoEquipamento grupoEquipamento);
        GrupoEquipamento ObterPorCodigo(int codigo);
    }
}
