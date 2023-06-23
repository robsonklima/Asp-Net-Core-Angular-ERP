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
        _logger.Info($"Iniciando o processamento: { Constants.SISTEMA_CAMADA_TASKS }");

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

            _logger.Info($"Finalizando o processamento: { Constants.SISTEMA_CAMADA_TASKS }");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFila() 
    {
        _logger.Info($"Iniciando o processamento da fila");

        var tipos = ObterCodigosTipos();

        foreach (int tipo in tipos)
        {
            SatTask task = ObterTask(tipo);

            var processar = podeProcessarTask(task);

            if (processar)
            {
                _logger.Info($"Criando uma nova task");

                var novaTask = CriarTask(tipo);

                _logger.Info($"Criada uma nova task: { novaTask?.CodSatTaskTipo }-{ novaTask?.Tipo?.Nome }");
            }
        }

        _logger.Info($"Finalizando o processamento da fila");
    }

    private async Task Processar() 
    {
        var tasks = ObterTasksPendentes();

        _logger.Info($"Iniciado o processamento das tasks: { tasks.Count() } registros");

        foreach (var task in tasks)
        {
            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BB)
            {
                _logger.Info($"Iniciando o processamento de: { Constants.INTEGRACAO_BB }");

                _integracaoBBService.Processar();
                
                AtualizarTask(task);

                _logger.Info($"Finalizado o processamento de: { Constants.INTEGRACAO_BB }");

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
            {
                _logger.Info($"Iniciando o processamento de: { Constants.INTEGRACAO_BANRISUL_ATM }");

                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornos();
                AtualizarTask(task);

                _logger.Info($"Finalizado o processamento de: { Constants.INTEGRACAO_BANRISUL_ATM }");

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
            {
                _logger.Info($"Iniciando o processamento de: { Constants.INTEGRACAO_BB }");

                await _integracaoZaffariService.ExecutarAsync();
                AtualizarTask(task);

                _logger.Info($"Finalizado o processamento de: { Constants.INTEGRACAO_BB }");

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_MRP)
            {
                _logger.Info($"Iniciando o processamento de: { Constants.INTEGRACAO_LOGIX_MRP }");

                _integracaoMRPService.ImportarArquivoMRPLogix();
                _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                AtualizarTask(task);

                _logger.Info($"Finalizado o processamento de: { Constants.INTEGRACAO_LOGIX_MRP }");
                continue;
            }   

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
            {
                _logger.Info($"Iniciando o processamento de: { Constants.ATUALIZACAO_PARQUE_MODELO }");

                _equipamentoContratoService.AtualizarParqueModelo();
                AtualizarTask(task);

                _logger.Info($"Finalizado o processamento de: { Constants.ATUALIZACAO_PARQUE_MODELO }");
                continue;
            }            
        }
    }

    private bool podeProcessarTask(SatTask task)
    {
        if (task is null) {
            _logger.Info($"Nenhuma task encontrada");

            return true;
        }

        if (task.IndProcessado == Constants.NAO_PROCESSADO)
        {
            _logger.Info($"Task { task.Tipo.Nome } pendente processamento");

            return false;
        }

        switch (task.CodSatTaskTipo)
        {
            case (int)SatTaskTipoEnum.INT_BANRISUL:
                _logger.Info($"Obtendo permissão para processar: { Constants.INTEGRACAO_BANRISUL_ATM } ");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_BB:
                _logger.Info($"Obtendo permissão para processar: { Constants.INTEGRACAO_BB } ");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_ZAFFARI:
                _logger.Info($"Obtendo permissão para processar: { Constants.INTEGRACAO_ZAFFARI } ");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_MRP:
                _logger.Info($"Obtendo permissão para processar: { Constants.INTEGRACAO_LOGIX_MRP } ");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                _logger.Info($"Obtendo permissão para processar: { Constants.ATUALIZACAO_PARQUE_MODELO } ");
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

        var codTipos = tipos.Select(x=> x.CodSatTaskTipo).OfType<int>().ToList();

        _logger.Info($"Obtidos { tipos.Count() } tipos de tasks para processar");

        return codTipos;
    }

    private SatTask ObterTask(int tipo) 
    {
        _logger.Info($"Obtendo a ultima task do tipo { tipo }");

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
        var tasks = (IEnumerable<SatTask>)_taskService.ObterPorParametros(new SatTaskParameters {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            SortActive = "CodSatTask",
            SortDirection = "DESC"
        })
        .Items;

        _logger.Info($"Obtendo tasks pendentes: { tasks.Count() }");

        return tasks;
    }

    private void AtualizarTask(SatTask task)
    {
        _logger.Info($"Atualizando a task");
        
        task.DataHoraProcessamento = DateTime.Now;
        task.IndProcessado = (byte)Constants.PROCESSADO;

        _taskService.Atualizar(task);
    }

    private SatTask CriarTask(int tipo) {
        _logger.Info($"Criando a task");

        return _taskService.Criar(new SatTask {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            DataHoraCad = DateTime.Now,
            CodSatTaskTipo = tipo
        });
    }
}