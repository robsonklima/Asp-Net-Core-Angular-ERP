using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDispBBRegiaoFilialRepository
    {
        List<DispBBRegiaoFilial> ObterPorParametros(DispBBRegiaoFilialParameters parameters);
    }
}