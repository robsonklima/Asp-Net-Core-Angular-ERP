using System.Threading.Tasks;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoSeniorService
    {
        Task<IntegracaoSenior> ExecutarAsync();
    }
}