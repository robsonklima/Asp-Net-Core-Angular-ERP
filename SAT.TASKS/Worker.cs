using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    private readonly IPlantaoTecnicoService _plantaoTecnicoService;
    private readonly IPontoUsuarioService _pontoUsuarioService;
    private readonly IIntegracaoFinanceiroService _integracaoFinanceiroService;
    private readonly IIntegracaoBanrisulService _integracaoBanrisulService;
    private readonly ISatTaskService _satTaskService;
    private readonly IEmailService _emailService;

    public Worker(
        IPlantaoTecnicoService plantaoTecnicoService,
        IPontoUsuarioService pontoUsuarioService,
        IIntegracaoFinanceiroService integracaoFinanceiroService,
        IIntegracaoBanrisulService integracaoBanrisulService,
        IEmailService emailService,
        ISatTaskService satTaskService
    )
    {
        _plantaoTecnicoService = plantaoTecnicoService;
        _pontoUsuarioService = pontoUsuarioService;
        _integracaoFinanceiroService = integracaoFinanceiroService;
        _integracaoBanrisulService = integracaoBanrisulService;
        _satTaskService = satTaskService;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornosAsync();

                if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL))
                    _plantaoTecnicoService.ProcessarTaskEmailsSobreavisoAsync();

                if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.CORRECAO_INTERVALOS_RAT))
                    _pontoUsuarioService.ProcessarTaskAtualizacaoIntervalosPontoAsync();
                    
                //_integracaoFinanceiroService.ExecutarAsync();
            }
            catch (Exception ex)
            {
                    
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}