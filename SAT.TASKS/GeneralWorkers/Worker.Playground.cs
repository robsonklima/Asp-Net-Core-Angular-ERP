using System.Diagnostics;
using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private async void IniciarPlaygroundAsync()
        {
            ExecutarProtegeAsync(new SatTask(), new List<OrdemServico>());
        }
    }
}