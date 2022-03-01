using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IEquipamentoService
    {
        ListViewModel ObterPorParametros(EquipamentoParameters parameters);
        Equipamento Criar(Equipamento equipamento);
        void Deletar(int codigo);
        void Atualizar(Equipamento equipamento);
        Equipamento ObterPorCodigo(int codigo);
    }
}
