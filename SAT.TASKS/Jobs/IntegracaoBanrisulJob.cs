using Quartz;
using SAT.SERVICES.Interfaces;

[DisallowConcurrentExecution]
public class IntegracaoBanrisulJob : IJob
{
    private readonly IServiceProvider _provider;
    private IIntegracaoBanrisulService _integracao;

    public IntegracaoBanrisulJob(IIntegracaoBanrisulService integracao)
    {
        _integracao = integracao;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _integracao.ProcessarEmailsAsync();
        _integracao.ProcessarRetornos();
        return Task.CompletedTask;
    }
}