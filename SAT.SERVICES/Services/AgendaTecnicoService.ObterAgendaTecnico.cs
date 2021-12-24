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
            List<AgendaTecnico> agendamentos = this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico);

            var pontos = this.ObterPontosDoDia(parameters);

            var agendamentosValidados = this.ValidaAgendamentos(agendamentos);
            agendamentosValidados.AddRange(pontos);

            AgendaTecnico intervalo = agendamentos.FirstOrDefault(i => i.Tipo == AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);
            AgendaTecnico fimExpediente = this.CriaFimDoExpediente(agendamentosValidados, parameters.CodTecnico.Value);

            if (intervalo != null) agendamentosValidados.Add(intervalo);
            if (fimExpediente != null) agendamentosValidados.Add(fimExpediente);

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

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, int? codTecnico) =>
           this._agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnico = codTecnico,
               IndAtivo = 1
           }).ToList();
        private List<AgendaTecnico> ValidaAgendamentos(List<AgendaTecnico> agendamentos)
        {
            List<AgendaTecnico> eventosValidados = new();
            List<AgendaTecnico> listaAtualizar = new();

            if (agendamentos.Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
               .Any(i => i.Fim.Date < DateTime.Now.Date && i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO))
            {
                var eventosRealocados = this.RealocaEventosComAtraso(agendamentos);
                eventosValidados.AddRange(eventosRealocados);
            }
            else
            {
                eventosValidados.AddRange(agendamentos.Where(i => i.IndAgendamento == 1));

                agendamentos
                .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
                .OrderBy(i => i.Inicio)
                .ToList().ForEach(i =>
                {
                    if (i.Fim.Date == DateTime.Now.Date && i.Fim < DateTime.Now)
                    {
                        i.Cor = GetStatusColor((StatusServicoEnum)i.OrdemServico.CodStatusServico);
                        i.CodUsuarioManut = Constants.SISTEMA_NOME;
                        i.DataHoraManut = DateTime.Now;
                        listaAtualizar.Add(i);
                        eventosValidados.Add(i);
                    }
                    else
                        eventosValidados.Add(i);
                });
            }

            this._agendaRepo.AtualizarListaAsync(listaAtualizar);
            return eventosValidados;
        }

        /// <summary>
        /// Roda via Agendamento
        /// </summary>
        public void CriaIntervalosDoDia()
        {
            Tecnico[] tecnicos = this._tecnicoRepo.ObterPorParametros(new TecnicoParameters()
            {
                Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                IndAtivo = 1
            }).Where(t => t.CodTecnico.GetValueOrDefault(0) != 0).ToArray();

            foreach (Tecnico tecnicoAtivo in tecnicos)
            {
                if (!this._agendaRepo.ExisteIntervaloNoDia(tecnicoAtivo.CodTecnico.Value))
                {
                    AgendaTecnico novoIntervalo = new AgendaTecnico
                    {
                        CodTecnico = tecnicoAtivo.CodTecnico,
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

                    this._agendaRepo.Criar(novoIntervalo);
                }
            }
        }

        private AgendaTecnico CriaFimDoExpediente(List<AgendaTecnico> agendamentos, int codTecnico)
        {
            var primeiroPontoDoDia =
                agendamentos
                .Where(i => i.CodTecnico == codTecnico && i.Tipo == AgendaTecnicoTypeEnum.PONTO && i.Inicio.Date == DateTime.Today.Date).OrderBy(i => i.Inicio).FirstOrDefault();

            if (primeiroPontoDoDia == null) return null;

            AgendaTecnico fimExpediente = new AgendaTecnico
            {
                CodTecnico = codTecnico,
                CodUsuarioCad = Constants.SISTEMA_NOME,
                DataHoraCad = DateTime.Now,
                Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.FIM_EXPEDIENTE),
                Tipo = AgendaTecnicoTypeEnum.FIM_EXPEDIENTE,
                Titulo = this.FimExpedienteTitle,
                Inicio = primeiroPontoDoDia.Inicio.AddHours(9).AddMinutes(48),
                Fim = primeiroPontoDoDia.Inicio.AddHours(9).AddMinutes(48),
                IndAgendamento = 0,
                IndAtivo = 1
            };

            return fimExpediente;
        }

        private List<AgendaTecnico> ObterPontosDoDia(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> pontos = new();

            List<PontoUsuario> pontosUsuario = this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters()
            {
                CodUsuario = parameters.CodUsuario,
                DataHoraRegistroInicio = DateTime.Today,
                DataHoraRegistroFim = DateTime.Today,
                IndAtivo = 1
            });

            pontosUsuario.Where(i => i.DataHoraRegistro.Date == DateTime.Now.Date).ToList().ForEach(p =>
            {
                pontos.Add(new AgendaTecnico
                {
                    CodTecnico = parameters.CodTecnico.Value,
                    Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                    Titulo = this.PontoTitle,
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
            List<AgendaTecnico> atualizarAgendas = new();

            agendasTecnico
            .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
            .OrderBy(i => i.Inicio)
            .ToList()
            .ForEach(e =>
            {
                if (e.OrdemServico.CodStatusServico != (int)StatusServicoEnum.TRANSFERIDO)
                {
                    e.Cor = this.GetStatusColor((StatusServicoEnum)e.OrdemServico.CodStatusServico);
                    atualizarAgendas.Add(e);
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

                    atualizarAgendas.Add(e);
                    ultimoEvento = e;
                    ultimaOS = os;
                }
            });

            this._agendaRepo.AtualizarListaAsync(atualizarAgendas);
            return agendasTecnico;
        }

        private string GetStatusColor(StatusServicoEnum statusServicoEnum)
        {
            switch (statusServicoEnum)
            {
                case StatusServicoEnum.FECHADO:
                    return "#4c4cff";
                case StatusServicoEnum.PECA_FALTANTE:
                    return "#ff4cb7";
                case StatusServicoEnum.PECAS_PENDENTES:
                    return "#ff4cb7";
                case StatusServicoEnum.PECA_EM_TRANSITO:
                    return "#ff4cb7";
                case StatusServicoEnum.PARCIAL:
                    return "#6dbd62";
                case StatusServicoEnum.CANCELADO:
                    return "#964B00";
                default:
                    return "#ff4c4c";
            }
        }
    }
}