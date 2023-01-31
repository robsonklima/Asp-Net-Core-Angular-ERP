using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IPecasLaboratorioRepository
    {
        PagedList<PecasLaboratorio> ObterPorParametros(PecasLaboratorioParameters parameters);
    }
}
