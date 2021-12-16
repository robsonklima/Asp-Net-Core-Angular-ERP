using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public AgendaTecnico[] OrdenarAgendaTecnico(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> agendamentos = new List<AgendaTecnico>();

            switch (parameters.Ordenacao)
            {
                case AgendaTecnicoOrdenationEnum.FIM_SLA:
                    agendamentos = this.OrdernarPorFimSLA(agendamentos);
                    break;
                case AgendaTecnicoOrdenationEnum.MENOR_TRAGETORIA:
                    agendamentos = this.OrdernarPorMenorTragetoria(agendamentos);
                    break;
            }

            return agendamentos.Distinct().ToArray();
        }

        private List<AgendaTecnico> OrdernarPorFimSLA(List<AgendaTecnico> agendamentos)
        {
            return agendamentos;
        }

        private List<AgendaTecnico> OrdernarPorMenorTragetoria(List<AgendaTecnico> agendamentos)
        {
            return agendamentos;
        }
    }
}