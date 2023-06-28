using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async Task IntegrarBanrisulAsync(SatTask task)
        {
            await _integracaoBanrisulService.ProcessarEmailsAsync();
            
            _integracaoBanrisulService.ProcessarRetornos();
        }
    }
}