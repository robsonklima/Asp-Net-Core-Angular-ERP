using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaCartaoCombustivelRepository
    {
        PagedList<DespesaCartaoCombustivel> ObterPorParametros(DespesaCartaoCombustivelParameters parameters);
    }
}
