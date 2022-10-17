using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IIntegracaoRepository
    {
        Integracao Criar(Integracao ordem);
        List<Integracao> ObterPorParametros(IntegracaoParameters parameters);
    }
}
