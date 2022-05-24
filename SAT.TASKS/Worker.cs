using NLog;

namespace SAT.TASKS
{
    public sealed class Worker : BackgroundService
    {
        public TesteEquip _te;

        public Worker(TesteEquip testeEquip)
        {
            _te = testeEquip;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _te.LogEquip();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}