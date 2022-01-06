using SAT.INFRA.Interfaces;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class MonitoramentoService : IMonitoramentoService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IOrdemServicoRepository _osRepository;

        public MonitoramentoService(IClienteRepository clienteRepository, IOrdemServicoRepository osRepository)
        {
            this._clienteRepository = clienteRepository;
            this._osRepository = osRepository;
        }
    }
}