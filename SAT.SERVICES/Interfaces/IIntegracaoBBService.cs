using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoBBService
    {
        Task ProcessarArquivosAsync();
        void ProcessarRetornos();
    }
}