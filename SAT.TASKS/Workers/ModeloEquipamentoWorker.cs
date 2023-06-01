using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS;
public partial class ModeloEquipamentoWorker : BackgroundService
{
    private readonly IContratoEquipamentoService _contratoEquipamentoService;
    private readonly IEquipamentoContratoService _equipamentoContratoService;
    public ModeloEquipamentoWorker (
        IContratoEquipamentoService contratoEquipamentoService,
        IEquipamentoContratoService equipamentoContratoService
    )
    {
        _contratoEquipamentoService = contratoEquipamentoService;
        _equipamentoContratoService = equipamentoContratoService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var contrParams = new ContratoEquipamentoParameters {};
                var contratos = _contratoEquipamentoService.ObterPorParametros(contrParams).Items;

                foreach (ContratoEquipamento contrato in contratos)
                {
                    var equipParams = new EquipamentoContratoParameters {
                        CodEquips = contrato.CodEquip.ToString(),
                        CodContratos = contrato.CodContrato.ToString(),
                        IndAtivo = 1
                    };

                    var equipamentos = _equipamentoContratoService.ObterPorParametros(equipParams).Items;
                    contrato.QtdEquipamentos = equipamentos.Count();
                    _contratoEquipamentoService.Atualizar(contrato);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
        }
    }
}