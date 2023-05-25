using System.Threading;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoProtegeService : IIntegracaoProtegeService
    {
        public string Executar()
        {
            Thread.Sleep(5000);

            return "";
        }
    }
}