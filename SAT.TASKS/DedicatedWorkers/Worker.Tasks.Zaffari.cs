using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async Task<SatTask> IntegrarZaffariAsync(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            var chamadosZaffari = chamados
                .Where(os => os.CodCliente == Constants.CLIENTE_ZAFFARI)
                .Where(os => os.IndIntegracao == 1)
                .Where(os => os.IndServico == 1);

            await _integracaoZaffariService.ExecutarAsync(chamadosZaffari);
            
            return task;
        }
    }
}