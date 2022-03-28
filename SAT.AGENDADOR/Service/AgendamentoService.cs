using System;

namespace SAT.AGENDADOR.Service
{
    public class AgendamentoService : Agendador
    {
        private static Startup services;

        public AgendamentoService()
        {
            services = new Startup();
            InicializaAgendamentos();
        }

        private static void InicializaAgendamentos()
        {
            DateTime agora = DateTime.Now;

            AgendarTarefa(nome: "Cria pontos no Agenda Técnico",
                          tarefa: () => services.AgendaTecnicoService.CriaIntervalosDoDia(),
                          intervaloEmSegundos: 30 * 60);
        }
    }
}
