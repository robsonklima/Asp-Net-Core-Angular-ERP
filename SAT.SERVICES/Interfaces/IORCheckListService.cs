using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IORCheckListService
    {
        ListViewModel ObterPorParametros(ORCheckListParameters parameters);
        ORCheckList Criar(ORCheckList checklist);
        void Deletar(int codigo);
        void Atualizar(ORCheckList checklist);
        ORCheckList ObterPorCodigo(int codigo);
    }
}
