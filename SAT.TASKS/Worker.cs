
using NLog;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IPlantaoTecnicoService _plantaoTecnicoService;
    private readonly ISatTaskService _satTaskService;

    public Worker(
        IPlantaoTecnicoService plantaoTecnicoService,
        ISatTaskService satTaskService
    ) {
        _plantaoTecnicoService = plantaoTecnicoService;
        _satTaskService = satTaskService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _plantaoTecnicoService.ProcessarTask();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}