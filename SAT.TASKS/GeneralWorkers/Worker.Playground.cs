namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlayground()
        {
            var chamado = _osService.ObterPorCodigo(8037388);

            var prazo = _ansService.CalcularPrazo(chamado);
        }
    }
}