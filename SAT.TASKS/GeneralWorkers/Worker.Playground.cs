using System.Diagnostics;
using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private void IniciarPlaygroundAsync()
        {
            ExecutarBB(new SatTask());
        }
    }
}