
using NLog;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS;
public class Worker : BackgroundService
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IEquipamentoContratoService _eqService;

    public Worker(
        IEquipamentoContratoService eqService) =>
        (_eqService) = (eqService);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var equip = _eqService.ObterPorCodigo(493530);
                _logger.Warn("Equipamento Encontrado: " + equip.Equipamento.NomeEquip);
            }
            catch (System.Exception ex)
            {
                _logger.Error("Caiu no Catch: " + ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}