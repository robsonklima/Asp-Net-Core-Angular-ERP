using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IContratoSLARepository
    {
        PagedList<ContratoSLA> ObterPorParametros(ContratoSLAParameters parameters);
    }
}
