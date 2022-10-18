using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORItemService
    {
        ListViewModel ObterPorParametros(ORItemParameters parameters);
        ORItem Criar(ORItem item);
        void Deletar(int codigo);
        void Atualizar(ORItem item);
        ORItem ObterPorCodigo(int codigo);
    }
}
