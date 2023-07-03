namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IniciarPlaygroundAsync()
        {
            var a = _ansService.CalcularPrazo(8042046);
        }
    }
}