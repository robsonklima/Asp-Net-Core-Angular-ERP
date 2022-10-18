using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoService
    {
        Integracao Criar(Integracao ordem);
        List<IntegracaoEquipamentoContrato> ConsultarEquipamentos(IntegracaoParameters parameters);
        List<Integracao> ConsultarOrdensServico(IntegracaoParameters parameters);
    }
}
