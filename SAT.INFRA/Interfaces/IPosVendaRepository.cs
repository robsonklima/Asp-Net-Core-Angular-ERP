using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPosVendaRepository
    {
        PagedList<PosVenda> ObterPorParametros(PosVendaParameters parameters);
        PosVenda ObterPorCodigo(int codigo);
    }
}