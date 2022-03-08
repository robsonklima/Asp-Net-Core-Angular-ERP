using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITipoFreteRepository
    {
        PagedList<TipoFrete> ObterPorParametros(TipoFreteParameters parameters);
    }
}
