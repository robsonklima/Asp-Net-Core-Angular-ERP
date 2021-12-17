using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
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
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico.Value);

            var pontos = this.ObterPontosDoDia(parameters);
            var intervalo = this.CriaIntervaloDoDia(agendamentos, parameters);
            var agendamentosValidados = this.ValidaAgendamentos(agendamentos);

            agendamentosValidados.AddRange(pontos);
            agendamentosValidados.Add(intervalo);

            return agendamentosValidados.Distinct().ToArray();
        }

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, string codTecnicos) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = DateTime.Now.DayOfWeek == DayOfWeek.Monday ? inicioPeriodo.AddDays(-7) : inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnicos = codTecnicos,
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
            List<AgendaTecnico> eventosValidados = new List<AgendaTecnico>();

            if (agendamentos.ToList()
               .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
               .Any(i => i.Fim.Date < DateTime.Now.Date && i.OrdemServico.CodStatusServico != (int)StatusServicoEnum.FECHADO))
            {
                var eventosRealocados = this.RealocaEventosComAtraso(agendamentos);
                eventosValidados.AddRange(eventosRealocados);
            }
            else
            {
                eventosValidados.AddRange(agendamentos.Where(i => i.IndAgendamento == 1).ToList());

                agendamentos.ToList()
                .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
                .OrderBy(i => i.Inicio)
                .ToList().ForEach(i =>
                {
                    if (i.Fim.Date == DateTime.Now.Date && i.Fim < DateTime.Now)
                    {
                        i.Cor = GetStatusColor(i.OrdemServico.CodStatusServico);
                        this._agendaRepo.Atualizar(i);
                        eventosValidados.Add(i);
                    }
                    else
                    {
                        eventosValidados.Add(i);
                    }
                });
            }

            return eventosValidados;
        }

        private AgendaTecnico CriaIntervaloDoDia(List<AgendaTecnico> agendamentos, AgendaTecnicoParameters parameters)
        {
            var intervaloDoDia =
                agendamentos
                .FirstOrDefault(i => i.Tipo == AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);

            if (intervaloDoDia != null) return intervaloDoDia;

            AgendaTecnico novoIntervalo = new AgendaTecnico
            {
                CodTecnico = parameters.CodTecnico,
                CodUsuarioCad = Constants.SISTEMA_NOME,
                DataHoraCad = DateTime.Now,
                Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.INTERVALO),
                Tipo = AgendaTecnicoTypeEnum.INTERVALO,
                Titulo = this.IntervaloTitle,
                Inicio = this.InicioIntervalo(),
                Fim = this.FimIntervalo(),
                IndAgendamento = 0,
                IndAtivo = 1
            };

            return novoIntervalo;
        }

        private List<AgendaTecnico> ObterPontosDoDia(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> pontos = new List<AgendaTecnico>();

            var pontosUsuario = this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
            {
                CodUsuario = parameters.CodUsuario,
                DataHoraRegistro = DateTime.Today.Date
            }).ToList();

            pontosUsuario.ForEach(p =>
            {
                pontos.Add(new AgendaTecnico
                {
                    CodTecnico = parameters.CodTecnico.Value,
                    Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                    Tipo = AgendaTecnicoTypeEnum.PONTO,
                    Inicio = p.DataHoraRegistro,
                    Fim = p.DataHoraRegistro.AddMilliseconds(25),
                    IndAgendamento = 0,
                    IndAtivo = 1
                });
            });

            return pontos;
        }

        // Realoca eventos devido a atraso
        private List<AgendaTecnico> RealocaEventosComAtraso(List<AgendaTecnico> agendasTecnico)
        {
            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico;

            AgendaTecnico ultimoEvento = null;
            OrdemServico ultimaOS = null;

            agendasTecnico
            .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
            .OrderBy(i => i.Inicio)
            .ToList()
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

                    // se termina durante o intervalo
                    var end = start.AddMinutes(duracao);
                    if (this.isIntervalo(end))
                    {
                        start = end.AddMinutes(deslocamento);
                        end = start.AddMinutes(duracao);
                    }

                    e.Inicio = start;
                    e.Fim = end;
                    e.CodUsuarioCad = Constants.SISTEMA_NOME;
                    e.DataHoraCad = DateTime.Now;
                    e.Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.OS);
                    var ag = this._agendaRepo.Atualizar(e);

                    ultimoEvento = ag;
                    ultimaOS = os;
                }

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