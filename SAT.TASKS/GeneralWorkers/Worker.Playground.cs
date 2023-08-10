using System.Diagnostics;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        [Conditional("DEBUG")]

        private async void IniciarPlaygroundAsync()
        {

            // var chamadosFechados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(new OrdemServicoParameters
            // {
            //     DataFechamentoInicio = DateTime.Now.AddMinutes(-5),
            //     DataFechamentoFim = DateTime.Now,
            //     Include = OrdemServicoIncludeEnum.OS_INTEGRACAO
            // }).Items;

            //  await ExecutarZaffariAsync(new SatTask(), chamadosFechados);
            
            ExecutarMRP(new SatTask());
        }
    }
}