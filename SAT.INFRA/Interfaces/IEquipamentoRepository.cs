using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IEquipamentoRepository
    {
        PagedList<Equipamento> ObterPorParametros(EquipamentoParameters parameters);
        Equipamento ObterPorCodigo(int codigo);
        void Criar(Equipamento equipamento);
        void Deletar(int codigo);
        void Atualizar(Equipamento equipamento);
    }
}
