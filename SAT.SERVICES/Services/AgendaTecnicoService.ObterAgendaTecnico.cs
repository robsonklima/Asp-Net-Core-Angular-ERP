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
            // !string.IsNullOrWhiteSpace(parameters.CodTecnicos) ?
            // this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnicos) :
            // this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico);

            // Tecnico tecnico = this._tecnicoRepo.ObterPorCodigo(parameters.CodTecnico.Value);
            // {
            //     Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
            //     CodTecnicos = parameters.CodTecnicos
            // });

            var pontos = this.ObterPontosDoDia(parameters);
            var agendamentosValidados = this.ValidaAgendamentos(agendamentos);
            agendamentosValidados.AddRange(pontos);

            // List<int> cods = new();
            // if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            // {
            //     cods.AddRange(parameters.CodTecnicos.Split(',').Select(s => int.Parse(s.Trim())).ToArray());
            // }
            // else
            // {
            //     cods.Add(parameters.CodTecnico.Value);
            // }

            // foreach (int codTec in cods)
            // {
            //     var intervalo = this.CriaIntervaloDoDia(agendamentos, codTec);
            //     var fimExpediente = this.CriaFimDoExpediente(agendamentosValidados, codTec);
            //     agendamentosValidados.Add(intervalo);
            //     if (fimExpediente != null) agendamentosValidados.Add(fimExpediente);
            // }

            var intervalo = this.CriaIntervaloDoDia(agendamentos, parameters.CodTecnico.Value);
            var fimExpediente = this.CriaFimDoExpediente(agendamentosValidados, parameters.CodTecnico.Value);
            agendamentosValidados.Add(intervalo);
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
                        //   this._agendaRepo.Atualizar(i);
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

        private AgendaTecnico CriaIntervaloDoDia(List<AgendaTecnico> agendamentos, int codTecnico)
        {
            var intervaloDoDia =
                agendamentos
                .FirstOrDefault(i => i.Tipo == AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);

            if (intervaloDoDia != null) return intervaloDoDia;

            AgendaTecnico novoIntervalo = new AgendaTecnico
            {
                CodTecnico = codTecnico,
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

            var ag = this._agendaRepo.Criar(novoIntervalo);

            return ag;
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
            //             List<PontoUsuario> pontosUsuario = new();
            //             Dictionary<string, int> dadosTecico = new();
            // 
            //             if (usuarios != null)
            //             {
            //                 foreach (Usuario usuario in usuarios.Where(i => i != null))
            //                 {
            //                     if (!dadosTecico.ContainsKey(usuario.CodUsuario.Trim().ToLower()))
            //                         dadosTecico.Add(usuario.CodUsuario.Trim().ToLower(), usuario.Tecnico.CodTecnico.Value);
            // 
            //                     pontosUsuario.AddRange(this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
            //                     {
            //                         CodUsuario = usuario.CodUsuario,
            //                         DataHoraRegistro = DateTime.Today.Date
            //                     }).ToList());
            //                 }
            //             }
            //             else
            //             {
            //                 dadosTecico.Add(parameters.CodUsuario.ToLower(), parameters.CodTecnico.Value);
            //                 pontosUsuario = this._pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
            //                 {
            //                     CodUsuario = parameters.CodUsuario,
            //                     DataHoraRegistro = DateTime.Today.Date
            //                 }).ToList();
            //             }

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

                    // var ag = this._agendaRepo.Atualizar(e);
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