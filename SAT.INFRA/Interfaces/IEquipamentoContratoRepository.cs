using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IEquipamentoContratoRepository
    {
        PagedList<EquipamentoContrato> ObterPorParametros(EquipamentoContratoParameters parameters);
        void Criar(EquipamentoContrato equipamentoContrato);
        void Deletar(int codigo);
        void Atualizar(EquipamentoContrato equipamentoContrato);
        EquipamentoContrato ObterPorCodigo(int codigo);
    }
}
