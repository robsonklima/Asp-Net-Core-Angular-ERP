using NLog;
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

            _logger.Info($"{ MsgConst.FIN_PROC } { Constants.SISTEMA_CAMADA_TASKS }");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFilaTasks() 
    {
        _logger.Info(MsgConst.INI_PROC_FILA);

        List<SatTaskTipo> tipos = (List<SatTaskTipo>)ObterTipos();

        foreach (var tipo in tipos)
        {
            SatTask task = ObterTask(tipo);

            var criar = deveCriarTask(task);

            if (criar)
            {
                _logger.Info(MsgConst.CRIANDO_TASK);

                var novaTask = CriarTask(tipo.CodSatTaskTipo);

                _logger.Info($"{ MsgConst.TASK_CRIADA } { novaTask.CodSatTask }, { novaTask.Tipo.Nome }");
            }
            else
            {
                _logger.Info($"{ MsgConst.EXISTE_TASK_PENDENTE } { task.CodSatTask }, { task.Tipo.Nome }");
            }
        }

        _logger.Info(MsgConst.FIN_PROC_FILA);
    }
    
    private void AtualizarFilaProcessos()
    {
        List<SatTaskTipo> tipos = ObterTipos(); 
        var processosPendentes = ObterProcessosPendentes();

        foreach (var tipo in tipos)
        {
            if (tipo.IndProcesso == 0)
            {
                _logger.Info($"{ MsgConst.TASK_NAO_POSSUI_PROCESSOS } { tipo.Nome }");

                continue;
            }

            _logger.Info($"{ MsgConst.OBTENDO_PROCESSOS } { tipo.Nome }");

            var parametros = new OrdemServicoParameters { 
                DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                DataHoraManutFim = DateTime.Now,
                CodClientes = ObterClientesProcessos()
            };
            var chamados = _osService.ObterPorParametros(parametros).Items;

            _logger.Info($"{ MsgConst.QTD_CHAMADOS_ENVIO } { chamados.Count() }");

            foreach (OrdemServico chamado in chamados)
            {
                var processo = ObterProcessoPendenteDoChamado(chamado.CodOS);

                if (processo is null)
                {
                     _logger.Info(MsgConst.CRIANDO_PROCESSO);

                    var p = new SatTaskProcesso
                    {
                        CodSatTaskTipo = tipo.CodSatTaskTipo,
                        DataHoraCad = DateTime.Now,
                        CodOS = chamado.CodOS,
                        Status = SatTaskStatusConst.PENDENTE,
                    };  

                    _taskProcessoService.Criar(p);

                    _logger.Info(MsgConst.PROCESSO_CRIADO);
                }
                else
                {
                    _logger.Info($"{ MsgConst.PROCESSO_PENDENTE } { processo.CodSatTaskProcesso }");
                }
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

    private bool deveCriarTask(SatTask task)
    {
        if (task is null) {
            _logger.Info(MsgConst.NENHUM_REG_ENCONTRADO);

            return true;
        }

        if (task.Status == SatTaskStatusConst.PENDENTE)
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
                _logger.Info($"{ MsgConst.OBTENDO_PERMISSAO } { Constants.INTEGRACAO_BANRISUL_ATM }");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BANRISUL_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_BB:
            _logger.Info($"{ MsgConst.OBTENDO_PERMISSAO } { Constants.INTEGRACAO_BB }");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BB_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_ZAFFARI:
            _logger.Info($"{ MsgConst.OBTENDO_PERMISSAO } { Constants.INTEGRACAO_ZAFFARI }");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_ZAFFARI_TEMPO_MIN);
            case (int)SatTaskTipoEnum.INT_MRP:
                _logger.Info($"{ MsgConst.OBTENDO_PERMISSAO } { Constants.INTEGRACAO_LOGIX_MRP }");
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_LOGIX_MRP_TEMPO_MIN);
            case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                _logger.Info($"{ MsgConst.OBTENDO_PERMISSAO } { Constants.ATUALIZACAO_PARQUE_MODELO }");
                return task.DataHoraProcessamento <= DateTime.Now.AddDays(-(int)Constants.ATUALIZACAO_PARQUE_MODELO_TEMPO_MIN);
            default:
                return false;
        }
    }

    private List<SatTaskTipo> ObterTipos()
    {
        _logger.Info(MsgConst.OBTENDO_TIPOS);
        
        List<SatTaskTipo> tipos = (List<SatTaskTipo>)_taskTipoService
            .ObterPorParametros(new SatTaskTipoParameters {
                IndAtivo = (byte)Constants.ATIVO
            })
            .Items;

        _logger.Info($"{ MsgConst.TIPOS_OBTIDOS } { tipos.Count() }");

        return tipos.ToList();
    }

    private SatTask ObterTask(SatTaskTipo tipo) 
    {
        _logger.Info($"{ MsgConst.OBTENDO_ULT_TASK } { tipo.Nome }");

        var task = (SatTask)_taskService
                .ObterPorParametros(new SatTaskParameters {
                    CodSatTaskTipo = tipo.CodSatTaskTipo,
                    SortActive = "CodSatTask",
                    SortDirection = "DESC"
                })
                .Items?
                .FirstOrDefault()!;

        if (task is not null)
            _logger.Info($"{ MsgConst.ULT_TASK_OBTIDA } { task.CodSatTask }");
        else 
            _logger.Info(MsgConst.REG_NAO_ENCONTRADO);

        return task!;
    }

    private IEnumerable<SatTask> ObterTasksPendentes()
    {
        _logger.Info(MsgConst.OBTENDO_TASKS_PENDENTES);
        
        var tasks = (IEnumerable<SatTask>)_taskService.ObterPorParametros(new SatTaskParameters {
            Status = SatTaskStatusConst.PENDENTE,
            SortActive = "CodSatTask",
            SortDirection = "DESC"
        })
        .Items;

        _logger.Info($"{ MsgConst.TASKS_OBTIDAS } { tasks.Count() }");

        return tasks;
    }

    private IEnumerable<SatTaskProcesso> ObterProcessosPendentes()
    {
        _logger.Info(MsgConst.OBTENDO_PROCESSOS_PENDENTES);
        
        var processos = (IEnumerable<SatTaskProcesso>)_taskProcessoService
            .ObterPorParametros(new SatTaskProcessoParameters {
                Status = SatTaskStatusConst.PENDENTE,
                SortActive = "CodSatTaskProcesso",
                SortDirection = "DESC"
            })
            .Items;

        _logger.Info($"{ MsgConst.QTD_PROCESSOS } { processos.Count() }");

        return processos;
    }

    private void AtualizarTask(SatTask task)
    {
        _logger.Info(MsgConst.ATUALIZANDO_TASK);
        
        task.DataHoraProcessamento = DateTime.Now;
        task.Status = SatTaskStatusConst.PROCESSADO;

        _taskService.Atualizar(task);

        _logger.Info($"{ MsgConst.TASK_ATUALIZADA } { task.CodSatTask }");
    }

    private SatTask CriarTask(int tipo) {
        _logger.Info(MsgConst.CRIANDO_TASK);

        var task = _taskService.Criar(new SatTask {
            Status = SatTaskStatusConst.PENDENTE,
            DataHoraCad = DateTime.Now,
            CodSatTaskTipo = tipo
        });

        _logger.Info($"{ MsgConst.TASK_CRIADA } { task.CodSatTask }");

        return task;
    }

    private string ObterClientesProcessos()
    {
        return $"{ Constants.CLIENTE_ZAFFARI }";
    }

    private SatTaskProcesso ObterProcessoPendenteDoChamado(int codOS)
    {
        _logger.Info($"{ MsgConst.OBTENDO_PROCESSOS_CHAMADO } { codOS }");

        var processos = (List<SatTaskProcesso>)_taskProcessoService
            .ObterPorParametros(new SatTaskProcessoParameters {
                CodOS = codOS,
                SortDirection = "DESC",
                SortActive = "CodSatTaskProcesso"
            }).Items;

        _logger.Info($"{ MsgConst.QTD_PROCESSOS } { processos.Count() }");

        return processos.FirstOrDefault()!;
    }
}