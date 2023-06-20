using Quartz;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.TASKS
{
    public class ModeloEquipamentoJob : IJob
    {
        private readonly ILogger<ModeloEquipamentoJob> _logger;

        private readonly IServiceProvider _provider;
        public ModeloEquipamentoJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using(var scope = _provider.CreateScope())
            {
                var contratoEquipService = scope.ServiceProvider.GetService<IContratoEquipamentoService>();
                var equipamentoContratoService = scope.ServiceProvider.GetService<IEquipamentoContratoService>();
                var contrParams = new ContratoEquipamentoParameters { };
                var contratos = contratoEquipService.ObterPorParametros(contrParams).Items;

                foreach (ContratoEquipamento contrato in contratos)
                {
                    var equipParams = new EquipamentoContratoParameters
                    {
                        CodEquips = contrato.CodEquip.ToString(),
                        CodContratos = contrato.CodContrato.ToString(),
                        IndAtivo = 1
                    };

                    contrato.QtdEquipamentos = equipamentoContratoService
                        .ObterPorParametros(equipParams)
                        .Items
                        .Count();

                    contratoEquipService.Atualizar(contrato);
                }
            }

            

            return Task.CompletedTask;
        }
    }
}