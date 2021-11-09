using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDispBBPercRegiaoRepository
    {
        List<DispBBPercRegiao> ObterPorParametros(DispBBPercRegiaoParameters parameters);
    }
}