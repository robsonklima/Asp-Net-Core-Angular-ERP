using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IImprodutividadeRepository
    {
        PagedList<Improdutividade> ObterPorParametros(ImprodutividadeParameters parameters);
        Improdutividade ObterPorCodigo(int CodImprodutividade);
    }
}
