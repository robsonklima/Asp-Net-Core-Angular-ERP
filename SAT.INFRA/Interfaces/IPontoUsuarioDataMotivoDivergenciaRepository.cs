using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoUsuarioDataMotivoDivergenciaRepository
    {
        PagedList<PontoUsuarioDataMotivoDivergencia> ObterPorParametros(PontoUsuarioDataMotivoDivergenciaParameters parameters);
    }
}