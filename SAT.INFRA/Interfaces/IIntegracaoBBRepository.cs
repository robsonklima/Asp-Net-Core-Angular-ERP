using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IIntegracaoBBRepository
    {
        PagedList<ViewIntegracaoBB> ObterPorParametros(IntegracaoBBParameters parameters);
    }
}
