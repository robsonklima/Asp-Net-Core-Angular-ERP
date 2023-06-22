using NLog;
using NLog.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IIntegracaoBanrisulService _integracaoBanrisulService;
    private readonly IEmailService _emailService;
    private readonly IIntegracaoZaffariService _integracaoZaffariService;
    private readonly ISatTaskService _taskService;
    private readonly ISatTaskTipoService _taskTipoService;
    private readonly IIntegracaoBBService _integracaoBBService;

    public Worker(
        ISatTaskService taskService,
        ISatTaskTipoService taskTipoService,
        IEmailService emailService,
        IIntegracaoBBService integracaoBBService,
        IIntegracaoBanrisulService integracaoBanrisulService,
        IIntegracaoZaffariService integracaoZaffariService
    )
    {
        _taskService = taskService;
        _taskTipoService = taskTipoService;
        _emailService = emailService;
        _integracaoBBService = integracaoBBService;
        _integracaoBanrisulService = integracaoBanrisulService;
        _integracaoZaffariService = integracaoZaffariService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        _logger.Info()
            .Message("Iniciado o processamento das Tasks", "")
            .Property("application", Constants.SISTEMA_CAMADA_TASKS)
            .Write();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                AtualizarFila();

                await Processar();
            }
            catch (Exception ex) {
                throw new Exception("Ocorreu um erro", ex);
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFila() 
    {
        _logger.Info()
            .Message("Iniciado o processamento da fila", "")
            .Property("application", Constants.SISTEMA_CAMADA_TASKS)
            .Write();

        var tipos = ObterCodigosTipos();

        foreach (int tipo in tipos)
        {
            var task = ObterTask(tipo);

            var processar = podeProcessarTask(task);

            if (processar)
            {
                _logger.Info()
                    .Message("Registrando a task para execução: {}", 
                        task?.Tipo?.Nome ?? "criando registro na fila")
                    .Property("application", Constants.SISTEMA_CAMADA_TASKS)
                    .Write();

                    CriarTask(tipo);
            }
        }
    }

    private async Task Processar() 
    {
        var tasks = ObterTasksPendentes();

        _logger.Info()
            .Message("Iniciado o processamento das tasks: {} registros", tasks.Count())
            .Property("application", Constants.SISTEMA_CAMADA_TASKS)
            .Write();

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
        }
    }

    private bool podeProcessarTask(SatTask task)
    {
        if (task is null)
            return true;

        if (task.IndProcessado == Constants.NAO_PROCESSADO)
            return false;

        switch (task.CodSatTaskTipo)
        {
            case (int)SatTaskTipoEnum.INT_BANRISUL:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_BB:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_ZAFFARI:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            default:
                return false;
        }
    }

    private List<int> ObterCodigosTipos()
    {
        List<SatTaskTipo> tipos = (List<SatTaskTipo>)_taskTipoService
            .ObterPorParametros(new SatTaskTipoParameters {
                IndAtivo = (byte)Constants.ATIVO
            })
            .Items;

        return tipos.Select(x=> x.CodSatTaskTipo).OfType<int>().ToList();
    }

    private SatTask ObterTask(int tipo) 
    {
        return (SatTask)_taskService
                .ObterPorParametros(new SatTaskParameters {
                    CodSatTaskTipo = tipo,
                    SortActive = "CodSatTask",
                    SortDirection = "DESC"
                })
                .Items?
                .FirstOrDefault()!;
    }

    private IEnumerable<SatTask> ObterTasksPendentes()
    {
        return (IEnumerable<SatTask>)_taskService.ObterPorParametros(new SatTaskParameters {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            SortActive = "CodSatTask",
            SortDirection = "DESC"
        })
        .Items;
    }

    private void AtualizarTask(SatTask task)
    {
        task.DataHoraProcessamento = DateTime.Now;
        task.IndProcessado = (byte)Constants.PROCESSADO;
        _taskService.Atualizar(task);
    }

    private SatTask CriarTask(int tipo) {
        return _taskService.Criar(new SatTask {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            DataHoraCad = DateTime.Now,
            CodSatTaskTipo = tipo
        });
    }
}