using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class IntegracaoBanrisulWorker : BackgroundService
{
    private readonly IIntegracaoBanrisulService _integracaoBanrisulService;

    public IntegracaoBanrisulWorker(
        IIntegracaoBanrisulService integracaoBanrisulService
    )
    {
        _integracaoBanrisulService = integracaoBanrisulService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornos();
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}