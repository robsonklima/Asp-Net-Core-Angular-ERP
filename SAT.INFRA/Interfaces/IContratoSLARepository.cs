using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoSLARepository
    {
        PagedList<ContratoSLA> ObterPorParametros(ContratoSLAParameters parameters);
    }
}
