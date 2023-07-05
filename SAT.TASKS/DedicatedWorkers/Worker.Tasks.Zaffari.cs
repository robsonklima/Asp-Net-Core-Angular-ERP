using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async Task<SatTask> ExecutarZaffariAsync(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            await _integracaoZaffariService.ExecutarAsync(chamados);
            
            return task;
        }
    }
}