using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
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
