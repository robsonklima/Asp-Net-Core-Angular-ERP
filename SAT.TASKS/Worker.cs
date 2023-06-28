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
                IniciarPlayground();

                var tipoParams = new SatTaskTipoParameters {
                    IndAtivo = (byte)Constants.ATIVO
                };
                var tipos = (List<SatTaskTipo>)_taskTipoService
                    .ObterPorParametros(tipoParams)
                    .Items;

                CriarFilaTasks(tipos);

                var taskParams = new SatTaskParameters { 
                    Status = SatTaskStatusConst.PENDENTE
                };
                var tasks = (IEnumerable<SatTask>)_taskService
                    .ObterPorParametros(taskParams)
                    .Items;

                await ProcessarFilaTasksAsync(tasks);
            }
            catch (Exception ex) {
                _logger.Error()
                    .Message(ex.Message)
                    .Property("application", Constants.SISTEMA_CAMADA_TASKS)
                    .Write();
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}