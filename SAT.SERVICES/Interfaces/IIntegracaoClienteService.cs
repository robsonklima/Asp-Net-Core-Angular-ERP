using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoClienteService
    {
        IntegracaoCliente Integrar(IntegracaoCliente data);
        IntegracaoCliente Atualizar(IntegracaoCliente data);
    }
}