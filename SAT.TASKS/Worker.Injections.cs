using NLog;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IIntegracaoBanrisulService _integracaoBanrisulService;
        private readonly IEmailService _emailService;
        private readonly IIntegracaoZaffariService _integracaoZaffariService;
        private readonly ISatTaskService _taskService;
        private readonly ISatTaskTipoService _taskTipoService;
        private readonly IIntegracaoBBService _integracaoBBService;
        private readonly IIntegracaoMRPService _integracaoMRPService;
        private readonly IEquipamentoContratoService _equipamentoContratoService;
        private readonly IOrdemServicoService _osService;

        public Worker(
            ISatTaskService taskService,
            ISatTaskTipoService taskTipoService,
            IEmailService emailService,
            IIntegracaoBBService integracaoBBService,
            IIntegracaoBanrisulService integracaoBanrisulService,
            IIntegracaoZaffariService integracaoZaffariService,
            IIntegracaoMRPService integracaoMRPService,
            IEquipamentoContratoService equipamentoContratoService,
            IOrdemServicoService osService
        )
        {
            _taskService = taskService;
            _taskTipoService = taskTipoService;
            _emailService = emailService;
            _integracaoBBService = integracaoBBService;
            _integracaoBanrisulService = integracaoBanrisulService;
            _integracaoZaffariService = integracaoZaffariService;
            _integracaoMRPService = integracaoMRPService;
            _equipamentoContratoService = equipamentoContratoService;
            _osService = osService;
        }
    }
}