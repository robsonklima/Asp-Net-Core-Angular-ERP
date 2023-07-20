using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAdendoItemService
    {
        ListViewModel ObterPorParametros(AdendoItemParameters parameters);
        AdendoItem Criar(AdendoItem data);
        AdendoItem Deletar(int codigo);
        AdendoItem Atualizar(AdendoItem data);
        AdendoItem ObterPorCodigo(int codigo);
    }
}
