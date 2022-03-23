using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
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

            var agendamentosAgrupados = agendamentos.GroupBy(i => i.CodTecnico).ToList();

            agendamentosAgrupados.ForEach(ags =>
            {
                var agendamentosDoTecnico = ags.ToList();

                if (agendamentosDoTecnico.Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS && i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO)
                   .Any(i => i.Fim.Date < DateTime.Now.Date))
                {
                    var eventosRealocados = this.ExcluirEventosComAtraso(agendamentosDoTecnico);
                    eventosValidados.AddRange(eventosRealocados);
                }
                else
                {
                    agendamentosDoTecnico.Where(i => i.IndAgendamento == 1 && i.Tipo == AgendaTecnicoTypeEnum.OS).ToList().ForEach(i =>
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
                            i.Cor = GetStatusColor((StatusServicoEnum)i.OrdemServico.CodStatusServico);
                            i.CodUsuarioManut = Constants.SISTEMA_NOME;
                            i.DataHoraManut = DateTime.Now;
                            listaAtualizar.Add(i);
                        }
                        else
                        {
                            i.Cor = this.AgendamentoColor;
                            listaAtualizar.Add(i);
                        }
                        eventosValidados.Add(i);
                    });

                    agendamentosDoTecnico.Where(i => i.IndAgendamento == 0 && i.Tipo == AgendaTecnicoTypeEnum.OS).ToList().ForEach(i =>
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
            });

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

        private List<AgendaTecnico> ExcluirEventosComAtraso(List<AgendaTecnico> agendasTecnico)
        {
            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico;
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
                    else if (e.Inicio.Date < DateTime.Now.Date && e.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO)
                    {
                        e.CodUsuarioManut = Constants.SISTEMA_NOME;
                        e.DataHoraManut = DateTime.Now;
                        e.IndAtivo =  0;
                        //_agendaRepo.Atualizar(e);

                        e.OrdemServico.CodStatusServico = (int)StatusServicoEnum.ABERTO;
                        e.OrdemServico.CodUsuarioManut = Constants.SISTEMA_NOME;
                        e.OrdemServico.DataHoraManut = DateTime.Now;
                        //_osRepo.Atualizar(e.OrdemServico);

                        _emailService.Enviar(new Email() {
                            Assunto = "Exclusão de Chamado da Agenda",
                            EmailDestinatario = "equipe.sat@perto.com.br",
                            EmailRemetente = "equipe.sat@perto.com.br",
                            Corpo = JsonConvert.SerializeObject(e),
                            NomeDestinatario = "SAT",
                            NomeRemetente = "SAT"
                        });
                    }
                });

            this._agendaRepo.AtualizarListaAsync(atualizarAgendas);
            return agendasTecnico.Where(i => i.IndAtivo == 1).ToList();
        }
    }
}