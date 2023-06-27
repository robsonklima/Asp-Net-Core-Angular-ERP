using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var tipos = ObterTipos();

                AtualizarFilaTasks(tipos);

                var tasks = ObterTasksPendentes();

                await ProcessarFilaTasksAsync(tasks);
            }
            catch (Exception ex) {
                 _logger.Error($"Ocorreu um erro { Constants.SISTEMA_CAMADA_TASKS }: { ex.Message }");

                Environment.Exit(1);
            }

            _logger.Info($"{ MsgConst.FIN_PROC } { Constants.SISTEMA_CAMADA_TASKS }");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}