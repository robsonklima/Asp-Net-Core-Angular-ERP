
using NLog;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IPlantaoTecnicoService _plantaoTecnicoService;
    private readonly IPontoUsuarioService _pontoUsuarioService;
    private readonly ISatTaskService _satTaskService;

    public Worker(
        IPlantaoTecnicoService plantaoTecnicoService,
        IPontoUsuarioService pontoUsuarioService,
        ISatTaskService satTaskService
    ) {
        _plantaoTecnicoService = plantaoTecnicoService;
        _pontoUsuarioService = pontoUsuarioService;
        _satTaskService = satTaskService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL))
                //     _plantaoTecnicoService.ProcessarTaskEmailsSobreaviso();

                if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.CORRECAO_INTERVALOS_RAT))
                    _pontoUsuarioService.ProcessarTaskAtualizacaoIntervalosPonto();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
        }
    }
}