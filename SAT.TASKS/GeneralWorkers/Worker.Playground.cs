using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlayground()
        {
            var chamado = _osService.ObterPorCodigo(8040219);

            var prazo = _ansService.CalcularPrazo(chamado);

            var osPrazo = new OSPrazoAtendimento {
                DataHoraLimiteAtendimento = prazo,
                CodOS = chamado.CodOS,
                DataHoraCad = DateTime.Now
            };

            osPrazo = (OSPrazoAtendimento)_prazoService.Criar(osPrazo);

            _logger.Info($"Calculado ANS para a OS: { osPrazo.CodOS }, id.: { osPrazo.CodOSPrazoAtendimento }");
        }
    }
}