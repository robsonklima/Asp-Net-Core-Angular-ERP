using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public void OrdenarAgendaTecnico(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> agendamentos =
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico.Value);

            switch (parameters.Ordenacao)
            {
                case AgendaTecnicoOrdenationEnum.FIM_SLA:
                    this.OrdernarPorFimSLA(agendamentos);
                    break;
                case AgendaTecnicoOrdenationEnum.MENOR_TRAGETORIA:
                    this.OrdernarPorMenorTragetoria(agendamentos);
                    break;
            }
        }

        private void OrdernarPorFimSLA(List<AgendaTecnico> agendamentos)
        {
            var agendamentosComSLA = agendamentos.Where(i => i.IndAgendamento == 0 &&
                i.Tipo == AgendaTecnicoTypeEnum.OS &&
                i.OrdemServico.PrazosAtendimento.Any())
            .OrderBy(i => i.OrdemServico.PrazosAtendimento
                .OrderByDescending(i => i.CodOSPrazoAtendimento)
                .FirstOrDefault().DataHoraLimiteAtendimento)
            .ToList();

            var agendamentosSemSLA = agendamentos.Where(i => i.IndAgendamento == 0 &&
                i.Tipo == AgendaTecnicoTypeEnum.OS &&
                !i.OrdemServico.PrazosAtendimento.Any())
            .OrderBy(i => i.Inicio).ToList();

            var listaAgendamentos = agendamentosComSLA.Concat(agendamentosSemSLA).ToList();

            this.ReordenaEventos(listaAgendamentos);
        }

        private void OrdernarPorMenorTragetoria(List<AgendaTecnico> agendamentos)
        {

        }

        private void ReordenaEventos(List<AgendaTecnico> agendasTecnico)
        {
            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico;

            AgendaTecnico ultimoEvento = null;
            OrdemServico ultimaOS = null;

            agendasTecnico
            .ForEach(e =>
            {
                if (e.OrdemServico.CodStatusServico == (int)StatusServicoEnum.FECHADO)
                {
                    e.Cor = this.GetStatusColor(e.OrdemServico.CodStatusServico);
                    this._agendaRepo.Atualizar(e);
                }
                else
                {
                    OrdemServico os = e.OrdemServico;

                    var deslocamento = this.DistanciaEmMinutos(os, ultimaOS);
                    var start = ultimoEvento != null ? ultimoEvento.Fim : this.InicioExpediente();
                    var duracao = (e.Fim - e.Inicio).TotalMinutes;

                    // adiciona deslocamento
                    start = start.AddMinutes(deslocamento);

                    // se começa durante o intervalo ou depois do expediente
                    if (this.isIntervalo(start))
                        start = this.FimIntervalo(start);
                    else if (start >= this.FimExpediente(start))
                    {
                        start = start.AddDays(1);
                        start = this.InicioExpediente(start);
                    }

                    // se termina durante a sugestao de intervalo
                    var end = start.AddMinutes(duracao);
                    if (this.isIntervalo(end))
                    {
                        start = end.AddMinutes(deslocamento);
                        end = start.AddMinutes(duracao);
                    }

                    e.Inicio = start;
                    e.Fim = end;
                    e.CodUsuarioCad = "ADMIN";
                    e.DataHoraCad = DateTime.Now;
                    e.Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.OS);
                    var ag = this._agendaRepo.Atualizar(e);

                    ultimoEvento = ag;
                    ultimaOS = os;
                }
            });
        }
    }
}