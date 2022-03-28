using SAT.SERVICES.Services;
using System;
using System.Timers;

namespace SAT.AGENDADOR
{
    public class Agendador
    {
        private static Timer agendamentoTimer;

        protected static void AgendarTarefa(string nome, Action tarefa, double intervaloEmSegundos)
        {
            agendamentoTimer = new();
            agendamentoTimer.Interval = intervaloEmSegundos * 1000;

            agendamentoTimer.Elapsed += delegate (Object source, ElapsedEventArgs e)
            {
                try
                {
                    LoggerService.LogInfo($"{DateTime.Now} - Inicializando tarefa: {nome}");
                    tarefa?.Invoke();
                }
                catch (Exception ex)
                {
                    LoggerService.LogInfo($"{DateTime.Now} - Erro ao executar tarefa: {ex.Message} : {ex.InnerException.Message}");
                }
                finally
                {
                    LoggerService.LogInfo($"{DateTime.Now} - Tarefa finalizada : {nome}");
                };
            };

            agendamentoTimer.AutoReset = true;
            agendamentoTimer.Enabled = true;
        }
    }
}
