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
                this.CriaIntervalosDoDia(agendamentos, parameters);

            agendamentos =
                this.ValidaAgendamentos(agendamentos);

            return agendamentos.Distinct().ToArray();
        }

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = DateTime.Now.DayOfWeek == DayOfWeek.Monday ? inicioPeriodo.AddDays(-7) : inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               IndAtivo = 1
           }).ToList();

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, int codTecnico) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnico = codTecnico,
               IndAtivo = 1
           }).ToList();

        private List<AgendaTecnico> ValidaAgendamentos(List<AgendaTecnico> agendamentos)
        {
            List<AgendaTecnico> eventosValidados =
            agendamentos.Where(i => i.Tipo != AgendaTecnicoTypeEnum.OS || i.IndAgendamento == 1).ToList();

            agendamentos
            .Where(i => i.Tipo == AgendaTecnicoTypeEnum.OS && i.IndAgendamento == 0)
            .GroupBy(i => i.CodTecnico)
            .ToList()
            .ForEach(eventosDoTecnico =>
            {
                eventosDoTecnico
                .OrderBy(i => i.Inicio)
                .ToList()
                .ForEach(i =>
                {
                    if (i.Tipo == AgendaTecnicoTypeEnum.OS && i.IndAgendamento == 0 && i.Fim.Date <= DateTime.Now.Date)
                    {
                        if (i.Fim.Date < DateTime.Now.Date && i.Fim < DateTime.Now)
                        {
                            if (i.OrdemServico.CodStatusServico != (int)StatusServicoEnum.FECHADO)
                            {
                                var eventosRealocados = this.RealocaEventosTecnico(eventosDoTecnico.ToList());
                                eventosValidados.AddRange(eventosRealocados);
                                return;
                            }
                        }
                        else if (i.Fim.Date == DateTime.Now.Date && i.Fim < DateTime.Now)
                        {
                            i.Cor = GetStatusColor(i.OrdemServico.CodStatusServico);
                            this._agendaRepo.Atualizar(i);
                        }

                        eventosValidados.Add(i);
                    }
                });
            });
            return eventosValidados;
        }

        private List<AgendaTecnico> CriaIntervalosDoDia(List<AgendaTecnico> agendamentos, AgendaTecnicoParameters parameters)
        {
            var codTecnicos = parameters.CodTecnicos?.Split(",").Select(i => i.Trim()).ToList() ?? null;

            if (codTecnicos == null) return agendamentos;

            var intervalosDoDia =
                agendamentos
                .Where(i => i.Tipo == AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);

            if (intervalosDoDia.Count() == codTecnicos.Count()) return agendamentos;

            codTecnicos.ForEach(codTec =>
            {
                var codTecnico = Convert.ToInt32(codTec);
                if (!intervalosDoDia.Any(i => i.CodTecnico == codTecnico))
                {
                    AgendaTecnico a = new AgendaTecnico
                    {
                        CodTecnico = codTecnico,
                        CodUsuarioCad = "ADMIN",
                        DataHoraCad = DateTime.Now,
                        Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.INTERVALO),
                        Tipo = AgendaTecnicoTypeEnum.INTERVALO,
                        Titulo = "INTERVALO",
                        Inicio = this.InicioIntervalo,
                        Fim = this.FimIntervalo,
                        IndAgendamento = 0,
                        IndAtivo = 1
                    };

                    var ag = this._agendaRepo.Criar(a);
                    agendamentos.Add(ag);
                }
            });
            return agendamentos;
        }

        // Realoca eventos devido a atraso
        private List<AgendaTecnico> RealocaEventosTecnico(List<AgendaTecnico> agendasTecnico)
        {
            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico;

            AgendaTecnico ultimoEvento = null;
            OrdemServico ultimaOS = null;

            agendasTecnico
            .Where(i => i.IndAgendamento == 0 && i.OrdemServico.CodStatusServico != (int)StatusServicoEnum.FECHADO)
            .OrderBy(i => i.Inicio)
            .ToList()
            .ForEach(e =>
            {
                OrdemServico os = e.OrdemServico;

                var deslocamento = this.DistanciaEmMinutos(os, ultimaOS);
                var start = ultimoEvento != null ? ultimoEvento.Fim : this.InicioExpediente;

                // se começa durante a sugestão de intervalo
                if (this.isIntervalo(start))
                    start = this.FimIntervalo;
                else if (start >= this.FimExpediente)
                {
                    start = start.AddDays(1);
                    if (this.isIntervalo(start))
                        start = new DateTime(start.Year, start.Month, start.Day, this.FimIntervalo.Hour, this.FimIntervalo.Minute, 0);
                }

                var duracao = (e.Fim - e.Inicio).TotalMinutes;

                // adiciona deslocamento
                start = start.AddMinutes(deslocamento);
                if (this.isIntervalo(start))
                    start = this.FimIntervalo;
                else if (start >= this.FimExpediente)
                {
                    start = start.AddDays(1);
                    if (this.isIntervalo(start))
                        start = new DateTime(start.Year, start.Month, start.Day, this.FimIntervalo.Hour, this.FimIntervalo.Minute, 0);
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
            });

            return agendasTecnico;
        }

        private string GetStatusColor(int statusOS)
        {
            switch (statusOS)
            {
                case 3:
                    return "#4c4cff";
                default:
                    return "#ff4c4c";
            }
        }
    }
}