using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IntegrarBB(SatTask task)
        {
            _integracaoBBService.Processar();
        }
    }
}