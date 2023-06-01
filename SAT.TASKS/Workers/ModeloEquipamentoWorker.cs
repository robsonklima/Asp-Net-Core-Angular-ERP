using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class ModeloEquipamentoWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
             
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
        }
    }
}