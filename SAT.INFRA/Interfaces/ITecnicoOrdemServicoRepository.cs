using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITecnicoOrdemServicoRepository
    {
        PagedList<Tecnico> ObterPorParametros(TecnicoOrdemServicoParameters parameters);
    }
}
