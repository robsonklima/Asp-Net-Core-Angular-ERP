using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface ITecnicoOrdemServicoRepository
    {
        PagedList<Tecnico> ObterPorParametros(TecnicoOrdemServicoParameters parameters);
    }
}
