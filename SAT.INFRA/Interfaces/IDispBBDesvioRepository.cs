using SAT.MODELS.Entities;
using System.Collections.Generic;


namespace SAT.INFRA.Interfaces
{
    public interface IDispBBDesvioRepository
    {
        List<DispBBDesvio> ObterPorParametros(DispBBDesvioParameters parameters);
    }
}