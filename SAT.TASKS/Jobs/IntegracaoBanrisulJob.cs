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

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _provider.CreateScope())
        {
            var jobType = context.JobDetail.JobType;
            var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

            // if (job != null) 
            // {
            //     var integracao = scope.ServiceProvider.GetService<IIntegracaoBanrisulService>();
            //     integracao.ProcessarEmailsAsync();
            //     integracao.ProcessarRetornos();
            // }

            await job.Execute(context);
        }
    }
}