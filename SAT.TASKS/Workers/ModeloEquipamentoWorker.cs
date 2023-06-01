using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS;
public partial class ModeloEquipamentoWorker : BackgroundService
{
    private readonly IContratoEquipamentoService _contratoEquipamentoService;
    public ModeloEquipamentoWorker (
        IContratoEquipamentoService contratoEquipamentoService)
    {
        _contratoEquipamentoService = contratoEquipamentoService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var parametros = new ContratoEquipamentoParameters {};
                var contratos = _contratoEquipamentoService.ObterPorParametros(parametros).Items;

                // Laco de repeticao para cada contrato equipamento
                // verificando se possui parque ativo. Inativar/Ativar conforme o gosto
            }
            catch (Exception)
            {
                    
            }

            await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
        }
    }
}