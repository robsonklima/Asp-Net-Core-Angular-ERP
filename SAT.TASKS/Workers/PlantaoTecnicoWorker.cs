using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class PlantaoTecnicoWorker : BackgroundService
{
    private readonly IPlantaoTecnicoService _plantaoTecnicoService;

    public PlantaoTecnicoWorker(
        IPlantaoTecnicoService plantaoTecnicoService
    )
    {
        _plantaoTecnicoService = plantaoTecnicoService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _plantaoTecnicoService.ProcessarTaskEmailsSobreavisoAsync();
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}