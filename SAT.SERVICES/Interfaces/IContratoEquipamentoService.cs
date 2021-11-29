using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoEquipamentoService
    {
        ListViewModel ObterPorParametros(ContratoEquipamentoParameters parameters);
        ContratoEquipamento ObterPorCodigo(int codigoContrato, int codEquip);
        void Criar(ContratoEquipamento contratoEquipamento);
        void Deletar(int codigoContrato, int codigoEquipamento);
        void Atualizar(ContratoEquipamento contratoEquipamento);

    }
}
