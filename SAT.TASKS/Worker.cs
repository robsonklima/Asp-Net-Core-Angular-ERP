using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                AtualizarFilaTasks();

                AtualizarFilaProcessos();

                await Processar();
            }
            catch (Exception ex) {
                 _logger.Error($"Ocorreu um erro { Constants.SISTEMA_CAMADA_TASKS }: { ex.Message }");

                Environment.Exit(1);
            }

            _logger.Info($"{ MsgConst.FIN_PROC } { Constants.SISTEMA_CAMADA_TASKS }");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
    
    private async Task Processar() 
    {
        var tasks = ObterTasksPendentes();

        _logger.Info(MsgConst.INI_PROC_TASKS);

        foreach (var task in tasks)
        {
            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BB)
            {
                _integracaoBBService.Processar();
                AtualizarTask(task);

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
            {
                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornos();
                AtualizarTask(task);

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
            {
                await _integracaoZaffariService.ExecutarAsync();
                AtualizarTask(task);

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_MRP)
            {
                _integracaoMRPService.ImportarArquivoMRPLogix();
                _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                AtualizarTask(task);

                continue;
            }   

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
            {
                _equipamentoContratoService.AtualizarParqueModelo();
                AtualizarTask(task);
                continue;
            }            
        }
    }
}