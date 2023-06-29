using NLog;
using NLog.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                IniciarPlaygroundAsync();

                var tipos = (List<SatTaskTipo>)_taskTipoService
                    .ObterPorParametros(new SatTaskTipoParameters { IndAtivo = (byte)Constants.ATIVO })
                    .Items;

                CriarFilaTasks(tipos);

                var tasks = (IEnumerable<SatTask>)_taskService
                    .ObterPorParametros(new SatTaskParameters { Status = SatTaskStatusConst.PENDENTE })
                    .Items;

                await ProcessarFilaTasksAsync(tasks);
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message(ex.Message)
                    .Property("application", Constants.SISTEMA_CAMADA_TASKS)
                    .Write();
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}