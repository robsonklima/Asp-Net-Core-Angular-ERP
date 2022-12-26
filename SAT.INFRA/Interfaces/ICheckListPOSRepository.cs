using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ICheckListPOSRepository {
        PagedList<CheckListPOS> ObterPorParametros(CheckListPOSParameters parameters);
        CheckListPOS ObterPorCodigo(int CodCheckListPOS);
        void Criar(CheckListPOS checkListPOS);
        void Deletar(int codigoCheckListPOS);
        void Atualizar(CheckListPOS checkListPOS);

    }
}