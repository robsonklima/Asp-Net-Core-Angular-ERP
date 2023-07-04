using System.Diagnostics;
using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private void IniciarPlaygroundAsync()
        {
            var task = new SatTask();
            
            ExecutarBB(task);
        }
    }
}