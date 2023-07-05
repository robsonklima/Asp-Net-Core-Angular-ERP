using System.Threading.Tasks;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoProtegeService
    {
        Task<ProtegeToken> LoginAsync();
        Task<OrdemServicoProtege> ConsultarChamadoAsync(ProtegeToken token, string numOSCliente, string busobId);
        Task<OrdemServicoProtege> EnviarChamadoAsync(ProtegeToken token);
        Task<OrdemServicoProtegeArmazenados> ConsultarPesquisaArmazenadaAsync(ProtegeToken token);
        Task<OrdemServicoProtege> AtualizarStatusChamadoAsync(ProtegeToken token);
    }
}