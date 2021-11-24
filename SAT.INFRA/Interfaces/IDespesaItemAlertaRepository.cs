using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaItemAlertaRepository
    {
        PagedList<DespesaItemAlerta> ObterPorParametros(DespesaItemAlertaParameters parameters);
    }
}
