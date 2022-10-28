using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORCheckListRepository
    {
        PagedList<ORCheckList> ObterPorParametros(ORCheckListParameters parameters);
        void Criar(ORCheckList cl);
        void Atualizar(ORCheckList cl);
        void Deletar(int cod);
        ORCheckList ObterPorCodigo(int cod);
    }
}
