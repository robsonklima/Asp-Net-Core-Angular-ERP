using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoClienteService
    {
        IntegracaoCliente Integrar(IntegracaoCliente data);
        List<IntegracaoCliente> ObterMeusIncidentes(IntegracaoClienteParameters par);
        List<EquipamentoCliente> ObterMeusEquipamentos(IntegracaoClienteParameters par);
    }
}