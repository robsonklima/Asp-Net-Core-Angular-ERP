using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ICheckListPOSItensRepository {
        PagedList<CheckListPOSItens> ObterPorParametros(CheckListPOSItensParameters parameters);
        CheckListPOSItens ObterPorCodigo(int CodCheckListPOSItens);
        void Criar(CheckListPOSItens checkListPOSItens);
        void Deletar(int codigoCheckListPOSItens);
        void Atualizar(CheckListPOSItens checkListPOSItens);

    }
}