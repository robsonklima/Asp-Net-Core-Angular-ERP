using Quartz;
using SAT.SERVICES.Interfaces;

[DisallowConcurrentExecution]
public class IntegracaoMRPJob : IJob
{
    private readonly IServiceProvider _provider;
    public IntegracaoMRPJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        using(var scope = _provider.CreateScope())
        {
            var integracao = scope.ServiceProvider.GetService<IIntegracaoMRPService>();

            integracao.ImportarArquivoMRPLogix();
            integracao.ImportarArquivoMRPEstoqueLogix();
        }
 
        return Task.CompletedTask;
    }
}