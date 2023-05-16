using System.Collections;
using System.Reflection;
using SAT.MODELS.Entities;
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
    private readonly ISatTaskService _satTaskService;
    private readonly IEmailService _emailService;
    private readonly IIntegracaoZaffariService _integracaoZaffariService;
    private readonly IIntegracaoCorreiosService _integracaoCorreiosService;
    private readonly IIntegracaoSemPararService _integracaoSemPararService;
    private readonly IIntegracaoProtegeService _integracaoProtegeService;
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
        ISatTaskService satTaskService
    )
    {
        _plantaoTecnicoService = plantaoTecnicoService;
        _pontoUsuarioService = pontoUsuarioService;
        _integracaoFinanceiroService = integracaoFinanceiroService;
        _integracaoBanrisulService = integracaoBanrisulService;
        _satTaskService = satTaskService;
        _integracaoCorreiosService = integracaoCorreiosService;
        _integracaoSeniorService = integracaoSeniorService;
        _integracaoSemPararService = integracaoSemPararService;
        _integracaoZaffariService = integracaoZaffariService;
        _integracaoProtegeService = integracaoProtegeService;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // await _integracaoBanrisulService.ProcessarEmailsAsync();
                // _integracaoBanrisulService.ProcessarRetornos();

                // if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL))
                //     _plantaoTecnicoService.ProcessarTaskEmailsSobreavisoAsync();

                // if (_satTaskService.PermitirExecucao(SatTaskTipoEnum.CORRECAO_INTERVALOS_RAT))
                //     _pontoUsuarioService.ProcessarTaskAtualizacaoIntervalosPontoAsync();

                _integracaoCorreiosService.Executar();
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}