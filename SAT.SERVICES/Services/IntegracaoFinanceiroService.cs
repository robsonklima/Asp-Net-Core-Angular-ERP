using SAT.INFRA.Interfaces;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoFinanceiroService : IIntegracaoFinanceiroService
    {
        private readonly IDefeitoRepository _defeitoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public IntegracaoFinanceiroService(

        )
        {

        }

        public void Executar()
        {
            throw new System.NotImplementedException();
        }
    }
}
