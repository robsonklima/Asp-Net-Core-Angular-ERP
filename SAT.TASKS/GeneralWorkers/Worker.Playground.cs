using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async Task IniciarPlaygroundAsync()
        {
            var chamados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(new OrdemServicoParameters
            {
                DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                DataHoraManutFim = DateTime.Now,
                CodCliente = Constants.CLIENTE_ZAFFARI
            }).Items;

           await IntegrarZaffariAsync(null, chamados);
        }
    }
}