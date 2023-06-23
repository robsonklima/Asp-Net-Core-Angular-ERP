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
    private readonly IIntegracaoMRPService _integracaoMRPService;
    private readonly IEquipamentoContratoService _equipamentoContratoService;

    public Worker(
        ISatTaskService taskService,
        ISatTaskTipoService taskTipoService,
        IEmailService emailService,
        IIntegracaoBBService integracaoBBService,
        IIntegracaoBanrisulService integracaoBanrisulService,
        IIntegracaoZaffariService integracaoZaffariService,
        IIntegracaoMRPService integracaoMRPService,
        IEquipamentoContratoService equipamentoContratoService
    )
    {
        _taskService = taskService;
        _taskTipoService = taskTipoService;
        _emailService = emailService;
        _integracaoBBService = integracaoBBService;
        _integracaoBanrisulService = integracaoBanrisulService;
        _integracaoZaffariService = integracaoZaffariService;
        _integracaoMRPService = integracaoMRPService;
        _equipamentoContratoService = equipamentoContratoService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        _logger.Info($"Iniciado o processamento das Tasks");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                AtualizarFila();

                await Processar();
            }
            catch (Exception ex) {
                 _logger.Error($"Ocorreu um erro { Constants.INTEGRACAO_BB }: { ex.Message }");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFila() 
    {
        _logger.Info($"Iniciando o processamento da fila");

        var tipos = ObterCodigosTipos();

        foreach (int tipo in tipos)
        {
            var task = ObterTask(tipo);

            var processar = podeProcessarTask(task);

            if (processar)
            {
                _logger.Info($"Executando a task");

                CriarTask(tipo);
            }
        }
    }

    private async Task Processar() 
    {
        var tasks = ObterTasksPendentes();

        _logger.Info($"Iniciado o processamento das tasks: { tasks.Count() } registros");

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
            case (int)SatTaskTipoEnum.INT_MRP:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                return task.DataHoraProcessamento <= DateTime.Now.AddDays(-1);
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