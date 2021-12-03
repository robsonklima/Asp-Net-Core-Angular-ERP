using System;

namespace SAT.AGENDADOR
{
    public class Tarefas
    {
        /// <summary>
        /// Prepara a tarefa/função para o agendamento
        /// </summary>
        /// <param name="nome">nome da tarefa - importante para o log</param>
        /// <param name="tarefa">tarefa/função a ser chamada</param>
        /// <param name="intervaloEmSegundos">intervalo de tempo em segundos</param>
        public static void Agendar(string nome, Action tarefa, double intervaloEmSegundos)
        {
            AgendamentoService.AgendarTarefa(nome, tarefa, intervaloEmSegundos);
        }
    }
}
