using SAT.MODELS.Entities;
using SAT.MODELS.Enums;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlayground()
        {
            var task = new SatTask {
                CodSatTaskTipo = (int)SatTaskTipoEnum.TESTE,
                DataHoraCad = DateTime.Now
            };

            var chamado = _osService.ObterPorCodigo(8037388);

            var prazo = _ansService.CalcularPrazo(chamado);
        }
    }
}