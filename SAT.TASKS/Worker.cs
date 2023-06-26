using NLog;
using NLog.Fluent;
using SAT.MODELS.Constants;
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
    private readonly ISatTaskProcessoService _taskProcessoService;
    private readonly IOrdemServicoService _osService;

    public Worker(
        ISatTaskService taskService,
        ISatTaskTipoService taskTipoService,
        IEmailService emailService,
        IIntegracaoBBService integracaoBBService,
        IIntegracaoBanrisulService integracaoBanrisulService,
        IIntegracaoZaffariService integracaoZaffariService,
        IIntegracaoMRPService integracaoMRPService,
        IEquipamentoContratoService equipamentoContratoService,
        ISatTaskProcessoService taskProcessoService,
        IOrdemServicoService osService
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
        _taskProcessoService = taskProcessoService;
        _osService = osService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        _logger.Info(MsgConst.INI_PROC + Constants.SISTEMA_CAMADA_TASKS);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                AtualizarFilaTasks();

                AtualizarFilaProcessos();

                await Processar();
            }
            catch (Exception ex) {
                 _logger.Error($"Ocorreu um erro { Constants.INTEGRACAO_BB }: { ex.Message }");
            }

            _logger.Info(MsgConst.FIN_PROC + Constants.SISTEMA_CAMADA_TASKS);

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFilaTasks() 
    {
        _logger.Info(MsgConst.INI_PROC_FILA);

        var tipos = ObterCodigosTipos();

        foreach (int tipo in tipos)
        {
            SatTask task = ObterTask(tipo);

            var processar = podeProcessarTask(task);

            if (processar)
            {
                _logger.Info(MsgConst.CRIANDO_TASK);

                var novaTask = CriarTask(tipo);

                _logger.Info(MsgConst.TASK_CRIADA + novaTask.Tipo.Nome);
            }
        }

        _logger.Info(MsgConst.FIN_PROC_FILA);
    }
    private void AtualizarFilaProcessos()
    {
        var tipos = ObterCodigosTipos(); 

        foreach (int tipo in tipos)
        {
            var parametros = new OrdemServicoParameters { 
                DataHoraInicioInicio = DateTime.Now.AddMinutes(-5),
                DataHoraInicioFim = DateTime.Now,
            };
            var chamados = _osService.ObterPorParametros(parametros).Items;

            _logger.Info(MsgConst.QTD_CHAMADOS_ENVIO + chamados.Count());

            foreach (OrdemServico chamado in chamados)
            {
                _logger.Info(MsgConst.CRIANDO_PROCESSO);

                var processo = new SatTaskProcesso
                {
                    CodSatTaskTipo = tipo,
                    DataHoraCad = DateTime.Now,
                    CodOS = chamado.CodOS,
                    IndProcessado = (byte)Constants.NAO_PROCESSADO,
                    Descricao = MsgConst.DESC_PROCESSO
                };  

                _logger.Info(MsgConst.PROCESSO_CRIADO + processo.CodSatTaskProcesso);

                _taskProcessoService.Criar(processo);
            }
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
                _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_BB);

                _integracaoBBService.Processar();
                
                AtualizarTask(task);

                _logger.Info(MsgConst.FIN_PROC + Constants.INTEGRACAO_BB);

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
            {
                _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_BANRISUL_ATM);

                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornos();
                AtualizarTask(task);

                _logger.Info(MsgConst.FIN_PROC + Constants.INTEGRACAO_BANRISUL_ATM);;

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
            {
                _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_ZAFFARI);

                await _integracaoZaffariService.ExecutarAsync();
                AtualizarTask(task);

                _logger.Info(MsgConst.FIN_PROC + Constants.INTEGRACAO_ZAFFARI);

                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_MRP)
            {
                _logger.Info(MsgConst.INI_PROC + Constants.INTEGRACAO_LOGIX_MRP);

                _integracaoMRPService.ImportarArquivoMRPLogix();
                _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                AtualizarTask(task);

                _logger.Info(MsgConst.FIN_PROC + Constants.INTEGRACAO_LOGIX_MRP);
                continue;
            }   

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
            {
                _logger.Info(MsgConst.INI_PROC + Constants.ATUALIZACAO_PARQUE_MODELO);

                _equipamentoContratoService.AtualizarParqueModelo();
                AtualizarTask(task);

                _logger.Info(MsgConst.FIN_PROC + Constants.ATUALIZACAO_PARQUE_MODELO);
                continue;
            }            
        }
    }

    private bool podeProcessarTask(SatTask task)
    {
        if (task is null) {
            _logger.Info(MsgConst.NENHUM_REG_ENCONTRADO);

            return true;
        }

        if (task.IndProcessado == Constants.NAO_PROCESSADO)
        {
            _logger.Info(MsgConst.TASK_PENDENTE);

            return false;
        }

        return ObterPermissaoProcessamento(task);
    }

    private bool ObterPermissaoProcessamento(SatTask task)
    {
        switch (task.CodSatTaskTipo)
        {
            case (int)SatTaskTipoEnum.INT_BANRISUL:
                _logger.Info(MsgConst.OBTENDO_PERMISSAO + Constants.INTEGRACAO_BANRISUL_ATM);
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BANRISUL_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_BB:
                _logger.Info(MsgConst.OBTENDO_PERMISSAO + Constants.INTEGRACAO_BB);
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BB_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_ZAFFARI:
                _logger.Info(MsgConst.OBTENDO_PERMISSAO + Constants.INTEGRACAO_ZAFFARI);
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_ZAFFARI_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_MRP:
                _logger.Info(MsgConst.OBTENDO_PERMISSAO + Constants.INTEGRACAO_LOGIX_MRP);
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_LOGIX_MRP_TEMPO_MIN);
            case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                _logger.Info(MsgConst.OBTENDO_PERMISSAO + Constants.ATUALIZACAO_PARQUE_MODELO);
                return task.DataHoraProcessamento <= DateTime.Now.AddDays(-(int)Constants.ATUALIZACAO_PARQUE_MODELO_TEMPO_MIN);
            default:
                return false;
        }
    }

    private List<int> ObterCodigosTipos()
    {
        _logger.Info(MsgConst.OBTENDO_TIPOS);
        
        List<SatTaskTipo> tipos = (List<SatTaskTipo>)_taskTipoService
            .ObterPorParametros(new SatTaskTipoParameters {
                IndAtivo = (byte)Constants.ATIVO
            })
            .Items;

        var codTipos = tipos.Select(x=> x.CodSatTaskTipo).OfType<int>().ToList();

        _logger.Info(MsgConst.TIPOS_OBTIDOS + tipos.Count());

        return codTipos;
    }

    private SatTask ObterTask(int tipo) 
    {
        _logger.Info(MsgConst.OBTENDO_ULT_TASK + tipo);

        var task = (SatTask)_taskService
                .ObterPorParametros(new SatTaskParameters {
                    CodSatTaskTipo = tipo,
                    SortActive = "CodSatTask",
                    SortDirection = "DESC"
                })
                .Items?
                .FirstOrDefault()!;

        if (task is not null)
            _logger.Info(MsgConst.ULT_TASK_OBTIDA + task.CodSatTask);
        else 
            _logger.Info(MsgConst.REG_NAO_ENCONTRADO);

        return task;
    }

    private IEnumerable<SatTask> ObterTasksPendentes()
    {
        _logger.Info(MsgConst.OBTENDO_TASKS);
        
        var tasks = (IEnumerable<SatTask>)_taskService.ObterPorParametros(new SatTaskParameters {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            SortActive = "CodSatTask",
            SortDirection = "DESC"
        })
        .Items;

        _logger.Info(MsgConst.TASKS_OBTIDAS + tasks.Count());

        return tasks;
    }

    private void AtualizarTask(SatTask task)
    {
        _logger.Info(MsgConst.ATUALIZANDO_TASK);
        
        task.DataHoraProcessamento = DateTime.Now;
        task.IndProcessado = (byte)Constants.PROCESSADO;

        _taskService.Atualizar(task);

        _logger.Info(MsgConst.TASK_ATUALIZADA + task.CodSatTask);
    }

    private SatTask CriarTask(int tipo) {
        _logger.Info(MsgConst.CRIANDO_TASK);

        var task = _taskService.Criar(new SatTask {
            IndProcessado = (byte)Constants.NAO_PROCESSADO,
            DataHoraCad = DateTime.Now,
            CodSatTaskTipo = tipo
        });

        _logger.Info(MsgConst.TASK_CRIADA + task.CodSatTask);

        return task;
    }
}