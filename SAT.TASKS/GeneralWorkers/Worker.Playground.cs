using System.Diagnostics;
using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private async void IniciarPlaygroundAsync()
        {
            // IEnumerable<OrdemServico> os = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(new MODELS.Entities.Params.OrdemServicoParameters { CodOS = "8066747" }).Items;
            // var task = new SatTask();

            //  await ExecutarZaffariAsync(task, os);

            ExecutarMRP(new SatTask());
        }
    }
}