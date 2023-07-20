using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAdendoItemRepository
    {
        PagedList<AdendoItem> ObterPorParametros(AdendoItemParameters parameters);
        AdendoItem Criar(AdendoItem data);
        AdendoItem Deletar(int cod);
        AdendoItem Atualizar(AdendoItem data);
        AdendoItem ObterPorCodigo(int cod);
    }
}
