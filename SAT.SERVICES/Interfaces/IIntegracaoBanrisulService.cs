using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoBanrisulService
    {
        Task ProcessarEmailsAsync();
        void ProcessarRetornos();
    }
}