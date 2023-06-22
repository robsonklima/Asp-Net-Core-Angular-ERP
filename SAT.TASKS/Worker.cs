using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public partial class Worker : BackgroundService
{
    private readonly IPlantaoTecnicoService _plantaoTecnicoService;
    private readonly IPontoUsuarioService _pontoUsuarioService;
    private readonly IIntegracaoFinanceiroService _integracaoFinanceiroService;
    private readonly IIntegracaoBanrisulService _integracaoBanrisulService;
    private readonly IEmailService _emailService;
    private readonly IIntegracaoZaffariService _integracaoZaffariService;
    private readonly IIntegracaoCorreiosService _integracaoCorreiosService;
    private readonly IIntegracaoSemPararService _integracaoSemPararService;
    private readonly IIntegracaoProtegeService _integracaoProtegeService;
    private readonly ISatTaskService _taskService;
    private readonly ISatTaskTipoService _taskTipoService;
    private readonly IIntegracaoBBService _integracaoBBService;
    private readonly IIntegracaoSeniorService _integracaoSeniorService;    

    public Worker(
        IPlantaoTecnicoService plantaoTecnicoService,
        IPontoUsuarioService pontoUsuarioService,
        IIntegracaoFinanceiroService integracaoFinanceiroService,
        IIntegracaoBanrisulService integracaoBanrisulService,
        IEmailService emailService,
        IIntegracaoCorreiosService integracaoCorreiosService,
        IIntegracaoSeniorService integracaoSeniorService,
        IIntegracaoZaffariService integracaoZaffariService,
        IIntegracaoSemPararService integracaoSemPararService,
        IIntegracaoProtegeService integracaoProtegeService,
        ISatTaskService taskService,
        ISatTaskTipoService taskTipoService,
        IIntegracaoBBService integracaoBBService
    )
    {
        _plantaoTecnicoService = plantaoTecnicoService;
        _pontoUsuarioService = pontoUsuarioService;
        _integracaoFinanceiroService = integracaoFinanceiroService;
        _integracaoBanrisulService = integracaoBanrisulService;
        _emailService = emailService;
        _integracaoCorreiosService = integracaoCorreiosService;
        _integracaoSeniorService = integracaoSeniorService;
        _integracaoSemPararService = integracaoSemPararService;
        _integracaoZaffariService = integracaoZaffariService;
        _integracaoProtegeService = integracaoProtegeService;
        _taskService = taskService;
        _taskTipoService = taskTipoService;
        _integracaoBBService = integracaoBBService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                AtualizarFila();
                await Processar();
            }
            catch (Exception) {}

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private void AtualizarFila() 
    {
        var tipos = ObterTipos();

        foreach (int tipo in tipos)
        {
            var task = ObterTask(tipo);

            if (VerificarNecessidadeProcessamento(task))
            {
                _taskService.Criar(new SatTask {
                    IndProcessado = (byte)Constants.PENDENTE,
                    DataHoraCad = DateTime.Now,
                    CodSatTaskTipo = tipo
                });
            }
        }
    }

    private async Task Processar() 
    {
        var tasks = ObterTasksPendentes();

        foreach (var task in tasks)
        {
            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
            {
                await _integracaoBanrisulService.ProcessarEmailsAsync();
                _integracaoBanrisulService.ProcessarRetornos();
                task.DataHoraProcessamento = DateTime.Now;
                task.IndProcessado = (byte)Constants.PROCESSADO;
                _taskService.Atualizar(task);
                continue;
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BB)
            {
                await _integracaoBBService.ProcessarsAsync();
            }

            if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
            {
                await _integracaoZaffariService.ExecutarAsync();
            }
        }
    }

    private bool VerificarNecessidadeProcessamento(SatTask task)
    {
        if (task == null)
            return true;

        if (task.IndProcessado == Constants.PENDENTE)
            return false;

        switch (task.CodSatTaskTipo)
        {
            case (int)SatTaskTipoEnum.INT_BANRISUL:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-5);
            case (int)SatTaskTipoEnum.INT_BB:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-10);
            case (int)SatTaskTipoEnum.INT_ZAFFARI:
                return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-10);
            default:
                return false;
        }
    }

    private List<int> ObterTipos()
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
            IndProcessado = (byte)Constants.PENDENTE,
            SortActive = "CodSatTask",
            SortDirection = "ASC"
        })
        .Items;
    }
}