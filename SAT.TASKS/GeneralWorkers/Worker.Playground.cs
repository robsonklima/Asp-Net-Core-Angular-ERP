using System.Diagnostics;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private void IniciarPlaygroundAsync()
        {
            IntegrarBB(null);
        }
    }
}