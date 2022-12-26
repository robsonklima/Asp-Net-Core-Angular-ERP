using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ICheckListPOSService
    {
        ListViewModel ObterPorParametros(CheckListPOSParameters parameters);
        CheckListPOS ObterPorCodigo(int codigo);
        void Criar(CheckListPOS checkListPOS);
        void Deletar(int codigoCheckListPOS);
        void Atualizar(CheckListPOS checkListPOS);
    }
}
