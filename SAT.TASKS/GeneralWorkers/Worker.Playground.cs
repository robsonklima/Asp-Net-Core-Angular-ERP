namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlaygroundAsync()
        {
            var chamado = _osService.ObterPorCodigo(8036742);

            var prazo = _ansService.CalcularPrazo(chamado);
        }
    }
}