using Quartz;
using SAT.SERVICES.Interfaces;

[DisallowConcurrentExecution]
public class IntegracaoBanrisulJob : IJob
{
    private readonly IServiceProvider _provider;
    public IntegracaoBanrisulJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        using(var scope = _provider.CreateScope())
        {
            var integracao = scope.ServiceProvider.GetService<IIntegracaoBanrisulService>();
            integracao.ProcessarEmailsAsync();
            integracao.ProcessarRetornos();
        }
 
        return Task.CompletedTask;
    }
}