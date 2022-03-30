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
        }
    }
}
