using System;

namespace SAT.AGENDADOR
{
    /*------------------------------------------ AGENDADOR --------------------------------------------------*
    |  Propósito: Agendar Tarefas para serem executadas em um período de tempo                               |
    |                                                                                                        |
    |  Usabilidade:                                                                                          |
    |      Startup -> 1) criar uma variável de acesso do repositório/serviço/controller/etc                  | 
    |                 2) Iniciar as dependências que serão usadas na função desejada em ConfigureServices()  |
    |                                                                                                        |     
    |      InicializaAgendamentos ->  Faz o agendamento da tarefa:                                           |
    |                      1) Tarefas.Agendar : função a ser chamada                                         |
    |                      2) A tarefa possui 3 parâmetros:                                                  |
    |                          a) Nome da tarefa : Importante para o LOG                                     |
    |                          b) Tarefa : a função em si a ser executada                                    |
    |                          c) Intervalo : de quanto em quanto tempo ela será executada (em segundos)     |
    *--------------------------------------------------------------------------------------------------------*/
    class Program
    {
        private static Startup services;

        public static void Main(string[] args)
        {
            services = new Startup();
            InicializaAgendamentos();
            Console.ReadKey();
        }

        /// <summary>
        /// Inicializa os agendamentos
        /// </summary>
        private static void InicializaAgendamentos()
        {
            // Inicializa os agendamentos
            DateTime agora = DateTime.Now;

            // Tarefa 1:
            Tarefas.Agendar(
            nome: "Atualiza dados dos indicadores Dashboard",
            tarefa: () => services.IndicadorService.AtualizaDadosIndicadoresDashboard(
                            periodoInicio: agora.AddDays(-30),
                            periodoFim: agora
            ),
            intervaloEmSegundos: 10 * 60); // 10 minutos

            // Tarefa 2:
            Tarefas.Agendar(
            nome: "Cria pontos no Agenda Técnico",
            tarefa: () => services.AgendaTecnicoService.CriaIntervalosDoDia(),
            intervaloEmSegundos: 5 * 60); // 60 minutos
        }
    }
}
