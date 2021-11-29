using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoEquipamentoRepository
    {
        PagedList<ContratoEquipamento> ObterPorParametros(ContratoEquipamentoParameters parameters);
        ContratoEquipamento ObterPorCodigo(int codContrato,int codEquip);
        void Criar(ContratoEquipamento contratoEquipamento);
        void Deletar(int codContrato,int codEquip);
        void Atualizar(ContratoEquipamento contratoEquipamento);
    }
}
