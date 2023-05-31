using System;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoClienteService : IIntegracaoClienteService
    {
        public IntegracaoCliente Integrar(IntegracaoCliente data)
        {
            int codCliente = UTILS.GenericHelper.ObterClientePorChave(data.Chave);

            OrdemServico os = new OrdemServico {
                DefeitoRelatado = data.RelatoCliente,
                NumOSQuarteirizada = data.Chave,
                CodCliente = codCliente
            };

            return data;
        }
    }
}