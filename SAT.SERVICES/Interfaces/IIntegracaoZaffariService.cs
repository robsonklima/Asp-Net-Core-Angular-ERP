using System.Collections.Generic;
using System.Threading.Tasks;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoZaffariService
    {
        Task ExecutarAsync(IEnumerable<OrdemServico> chamados);
    }
}