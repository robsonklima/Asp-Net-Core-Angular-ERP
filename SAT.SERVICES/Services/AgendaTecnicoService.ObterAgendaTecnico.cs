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
                !string.IsNullOrWhiteSpace(parameters.CodTecnicos) ?
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnicos) :
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico);

            List<Usuario> usuarios = new();

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                List<Tecnico> tecnicos = this._tecnicoRepo.ObterPorParametros(new TecnicoParameters()
                {
                    Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                    CodTecnicos = parameters.CodTecnicos
                });

                usuarios.AddRange(tecnicos.Select(u => u.Usuario));
            }
            else if (parameters.CodTecnico.HasValue)
            {
                Tecnico tecnico = this._tecnicoRepo.ObterPorCodigo(parameters.CodTecnico.Value);
                usuarios.Add(tecnico.Usuario);
            }

            var pontos = this.ObterPontosDoDia(parameters, usuarios);
            var agendamentosValidados = this.ValidaAgendamentos(agendamentos);
            agendamentosValidados.AddRange(pontos);

            List<int> cods = new();
            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                cods.AddRange(parameters.CodTecnicos.Split(',').Select(s => int.Parse(s.Trim())).ToArray());
            }
            else
            {
                cods.Add(parameters.CodTecnico.Value);
            }

            foreach (int codTec in cods)
            {
                AgendaTecnico intervalo = agendamentos.FirstOrDefault(i => i.CodTecnico == codTec && i.Tipo == AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);
                AgendaTecnico fimExpediente = this.CriaFimDoExpediente(agendamentosValidados, codTec);

                if (fimExpediente != null) agendamentosValidados.Add(fimExpediente);
                if (intervalo != null) agendamentosValidados.Add(intervalo);
            }

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
                agendamentos.Where(i => i.IndAgendamento == 1 && i.Tipo == AgendaTecnicoTypeEnum.OS).ToList().ForEach(i =>
                {
                    if ((i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.CANCELADO) || (i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.ABERTO))
                    {
                        i.IndAtivo = 0;
                        i.CodUsuarioManut = Constants.SISTEMA_NOME;
                        i.DataHoraManut = DateTime.Now;
                        listaAtualizar.Add(i);
                    }
                    else if ((i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.FECHADO))
                    {
                        i.CodUsuarioManut = Constants.SISTEMA_NOME;
                        i.DataHoraManut = DateTime.Now;
                        i.Cor = GetStatusColor((StatusServicoEnum)i.OrdemServico.CodStatusServico);
                        listaAtualizar.Add(i);
                    }

                    eventosValidados.Add(i);
                });

                agendamentos
                .Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS)
                .ToList().ForEach(i =>
                {
                    if (i.Fim < DateTime.Now)
                    {
                        if ((i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.CANCELADO) || (i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.ABERTO))
                            i.IndAtivo = 0;

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

            // Verifica duplicados
            eventosValidados.Where(i => i.Tipo == AgendaTecnicoTypeEnum.OS)
            .GroupBy(i => i.CodOS)
            .ToList()
            .Where(i => i.Key > 1)
            .ToList().ForEach(i =>
           {
               i.Skip(1).ToList().ForEach(a =>
               {
                   a.IndAtivo = 0;
                   a.CodUsuarioManut = Constants.SISTEMA_NOME;
                   a.DataHoraManut = DateTime.Now;
                   listaAtualizar.Add(a);
               });
           });

            this._agendaRepo.AtualizarListaAsync(listaAtualizar);
            return eventosValidados.Where(i => i.IndAtivo == 1).ToList();
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

        private List<AgendaTecnico> ObterPontosDoDia(AgendaTecnicoParameters parameters, IEnumerable<Usuario> usuarios = null)
        {
            List<AgendaTecnico> pontos = new();

            var distinctUsers = usuarios.Where(i => i != null).Distinct();

            if (distinctUsers != null)
            {
                foreach (Usuario usuario in distinctUsers)
                {
                    var pontosUsuario = this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
                    {
                        CodUsuario = usuario.CodUsuario,
                        DataHoraRegistro = DateTime.Today.Date,
                        IndAtivo = 1
                    }).ToList();

                    pontosUsuario.ForEach(p =>
                    {
                        pontos.Add(new AgendaTecnico
                        {
                            CodTecnico = usuario?.CodTecnico,
                            Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                            Tipo = AgendaTecnicoTypeEnum.PONTO,
                            Titulo = this.PontoTitle,
                            Inicio = p.DataHoraRegistro,
                            Fim = p.DataHoraRegistro.AddMilliseconds(25),
                            IndAgendamento = 0,
                            IndAtivo = 1
                        });
                    });
                }
            }
            else
            {
                var pontosUsuario = this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
                {
                    CodUsuario = parameters.CodUsuario,
                    DataHoraRegistro = DateTime.Today.Date,
                    IndAtivo = 1
                }).ToList();

                pontosUsuario.ForEach(p =>
                    {

                        pontos.Add(new AgendaTecnico
                        {
                            CodTecnico = parameters?.CodTecnico,
                            Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                            Titulo = this.PontoTitle,
                            Tipo = AgendaTecnicoTypeEnum.PONTO,
                            Inicio = p.DataHoraRegistro,
                            Fim = p.DataHoraRegistro.AddMilliseconds(25),
                            IndAgendamento = 0,
                            IndAtivo = 1
                        });
                    });
            }

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
                    if ((e.OrdemServico.CodStatusServico == (int)StatusServicoEnum.CANCELADO) || (e.OrdemServico.CodStatusServico == (int)StatusServicoEnum.ABERTO))
                        e.IndAtivo = 0;

                    e.CodUsuarioManut = Constants.SISTEMA_NOME;
                    e.DataHoraManut = DateTime.Now;
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
            return agendasTecnico.Where(i => i.IndAtivo == 1).ToList();
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