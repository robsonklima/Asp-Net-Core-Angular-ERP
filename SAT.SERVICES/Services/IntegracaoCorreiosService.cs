using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoCorreiosService : IIntegracaoCorreiosService
    {
        private IInstalacaoService _instService;

        public IntegracaoCorreiosService(IInstalacaoService instService)
        {
            _instService = instService;
        }

        public void Executar()
        {
         
        }
    }
}