using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public AgendaTecnico[] ObterAgendaTecnico(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> agendamentos =
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value);

            agendamentos =
                this.ValidaAgendamentos(agendamentos);

            return agendamentos.ToArray();
        }

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo
           }).ToList();

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, int codTecnico) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnico = codTecnico
           }).ToList();

        private List<AgendaTecnico> ValidaAgendamentos(List<AgendaTecnico> agendamentos) =>
            agendamentos.Select(i =>
            {
                if (i.Tipo == AgendaTecnicoTypeEnum.OS && i.CodOS.HasValue && i.IndAgendamento == 0 && i.Fim < DateTime.Now)
                {
                    var os = this._osRepo.ObterPorCodigo(i.CodOS.Value);
                    i.Cor = GetStatusColor(os.CodStatusServico);
                    this._agendaRepo.Atualizar(i);
                }
                return i;
            }).ToList();

        private string GetStatusColor(int statusOS)
        {
            switch (statusOS)
            {
                case 3:
                    return "#7f7fff";
                default:
                    return "#ff4c4c";
            }
        }
    }
}