using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IDespesaCartaoCombustivelRepository
    {
        PagedList<DespesaCartaoCombustivel> ObterPorParametros(DespesaCartaoCombustivelParameters parameters);
    }
}
