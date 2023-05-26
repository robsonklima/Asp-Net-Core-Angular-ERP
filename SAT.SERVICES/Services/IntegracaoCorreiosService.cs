using System;
using System.Threading;
using System.Threading.Tasks;
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

        public string Executar()
        {
            Thread.Sleep(1000);

            return "";
        }
    }
}