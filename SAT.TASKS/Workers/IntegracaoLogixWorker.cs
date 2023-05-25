using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class IntegracaoLogixWorker : BackgroundService
{
    private readonly IIntegracaoLogixService _integracaoLogixService;

    public IntegracaoLogixWorker(
        IIntegracaoLogixService integracaoLogixService
    )
    {
        _integracaoLogixService = integracaoLogixService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _integracaoLogixService.ImportarArquivoLogix();
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromMinutes(120), stoppingToken);
        }
    }
}