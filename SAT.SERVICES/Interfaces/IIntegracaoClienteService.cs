using System.Collections.Generic;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoClienteService
    {
        IntegracaoCliente Integrar(IntegracaoCliente data);
        List<LocalAtendimentoCliente> ObterLocais(IntegracaoCliente data);
    }
}