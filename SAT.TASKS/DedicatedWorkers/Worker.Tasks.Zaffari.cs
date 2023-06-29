using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async Task<SatTask> IntegrarZaffariAsync(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            await _integracaoZaffariService.ExecutarAsync(chamados);
            
            return task;
        }
    }
}