using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaAdiantamentoTipoRepository
    {
        PagedList<DespesaAdiantamentoTipo> ObterPorParametros(DespesaAdiantamentoTipoParameters parameters);
    }
}