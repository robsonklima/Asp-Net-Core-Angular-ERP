namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlaygroundAsync()
        {
            var prazo = _ansService.CalcularPrazo(8025633);            
        }
    }
}