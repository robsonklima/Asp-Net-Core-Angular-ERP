using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoUsuarioDataMotivoDivergenciaService
    {
        ListViewModel ObterPorParametros(PontoUsuarioDataMotivoDivergenciaParameters parameters);
    }
}