using Quartz;
using SAT.SERVICES.Interfaces;

[DisallowConcurrentExecution]
public class IntegracaoBBJob : IJob
{
    private readonly IServiceProvider _provider;

    public IntegracaoBBJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        using(var scope = _provider.CreateScope())
        {
            var integracao = scope.ServiceProvider.GetService<IIntegracaoBBService>();
        }
 
        return Task.CompletedTask;
    }
}