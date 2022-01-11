using SAT.AGENDADOR.Service;
using System;
using Topshelf;

namespace SAT.AGENDADOR
{
    /*------------------------------------------ AGENDADOR --------------------------------------------------*
    |  Propósito: Agendar Tarefas para serem executadas em um período de tempo                               |
    |                                                                                                        |
    |  Usabilidade:                                                                                          |
    |      Startup -> 1) criar uma variável de acesso do repositório/serviço/controller/etc                  | 
    |                 2) Iniciar as dependências que serão usadas na função desejada em ConfigureServices()  |
    |                                                                                                        |     
    |      AgendamentoService.InicializaAgendamentos ->  Faz o agendamento da tarefa:                        |
    |                      1) Tarefas.Agendar : função a ser chamada                                         |
    |                      2) A tarefa possui 3 parâmetros:                                                  |
    |                          a) Nome da tarefa : Importante para o LOG                                     |
    |                          b) Tarefa : a função em si a ser executada                                    |
    |                          c) Intervalo : de quanto em quanto tempo ela será executada (em segundos)     |
    *--------------------------------------------------------------------------------------------------------*/
    class Program
    {
        public static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<AgendamentoService>(s =>
                {
                    s.ConstructUsing(serv => new AgendamentoService());
                    s.WhenStarted(s => { });
                    s.WhenStopped(s => { });
                });

                x.RunAsLocalService();

                x.SetServiceName("AgendadorSAT");
                x.SetDisplayName("Agendador de Tarefas SAT");
                x.SetDescription("Agenda funções, métodos, serviços em rotinas determinadas do SAT");
            });

            int exitCodValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodValue;
        }
    }
}
