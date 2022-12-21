using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ICheckListPOSItensService
    {
        ListViewModel ObterPorParametros(CheckListPOSItensParameters parameters);
        CheckListPOSItens ObterPorCodigo(int codigo);
        void Criar(CheckListPOSItens checkListPOSItens);
        void Deletar(int codigoCheckListPOSItens);
        void Atualizar(CheckListPOSItens checkListPOSItens);
    }
}
