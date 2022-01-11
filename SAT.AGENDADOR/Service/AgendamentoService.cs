using System;

namespace SAT.AGENDADOR.Service
{
    public class AgendamentoService : Agendador
    {
        /// <summary>
        /// Instancia dos serviços
        /// </summary>
        private static Startup services;

        // Inicializador
        public AgendamentoService()
        {
            services = new Startup();
            InicializaAgendamentos();
        }

        /// <summary>
        /// Inicializa os agendamentos
        /// </summary>
        private static void InicializaAgendamentos()
        {
            // Inicializa os agendamentos
            DateTime agora = DateTime.Now;

            // Tarefa 1:
            AgendarTarefa(
            nome: "Atualiza dados dos indicadores Dashboard",
            tarefa: () => services.IndicadorService.AtualizaDadosIndicadoresDashboard(
                            periodoInicio: agora.AddDays(-1),
                            periodoFim: agora
            ),
            intervaloEmSegundos: 10 * 60); // 10 minutos

            // Tarefa 2:
            AgendarTarefa(
            nome: "Cria pontos no Agenda Técnico",
            tarefa: () => services.AgendaTecnicoService.CriaIntervalosDoDia(),
            intervaloEmSegundos: 30 * 60); // 30 minutos
        }
    }
}
