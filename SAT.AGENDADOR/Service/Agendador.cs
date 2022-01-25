using SAT.SERVICES.Services;
using System;
using System.Timers;

namespace SAT.AGENDADOR
{
    public class Agendador
    {
        // variavel estatica de Timer
        private static Timer agendamentoTimer;

        /// <summary>
        /// Inicializa uma tarefa/função para ser chamada em um intervalo de tempo
        /// </summary>
        /// <param name="task">tarefa/função a ser chamada</param>
        /// <param name="intervaloEmSegundos">intervalo de tempo em segudos</param>
        protected static void AgendarTarefa(string nome, Action tarefa, double intervaloEmSegundos)
        {
            tarefa?.Invoke();
            // Cria um temporizador com o valor de intervalo em segundos
            agendamentoTimer = new();
            agendamentoTimer.Interval = intervaloEmSegundos * 1000;

            // Seta a task para o cronômetro. 
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

            // Faz o cronômetro disparar eventos repetidos
            agendamentoTimer.AutoReset = true;

            // Inicializa o timer
            agendamentoTimer.Enabled = true;
        }
    }
}
