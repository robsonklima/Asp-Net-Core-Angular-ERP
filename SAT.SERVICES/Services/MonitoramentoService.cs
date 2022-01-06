using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class MonitoramentoService : IMonitoramentoService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IOrdemServicoRepository _osRepository;
        private readonly ILogAlertaRepository _logAlertRepository;

        public MonitoramentoService(IClienteRepository clienteRepository, IOrdemServicoRepository osRepository, ILogAlertaRepository logAlertRepository)
        {
            this._clienteRepository = clienteRepository;
            this._osRepository = osRepository;
            this._logAlertRepository = logAlertRepository;
        }
        public Monitoramento[] ObterPorParametros(MonitoramentoParameters parameters)
        {
            switch (parameters.Tipo)
            {
                case MonitoramentoTipoEnum.CLIENTE:
                    return ObterPorClientes(parameters);
                case MonitoramentoTipoEnum.SERVICO:
                    return ObterPorServicos(parameters);
                default:
                    return new Monitoramento[] { };
            }
        }
    }
}