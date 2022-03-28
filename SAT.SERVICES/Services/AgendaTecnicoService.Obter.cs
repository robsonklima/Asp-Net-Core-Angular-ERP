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
            List<AgendaTecnico> agendas = ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnicos);

            List<Usuario> usuarios = new();

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                List<Tecnico> tecnicos = _tecnicoRepo.ObterPorParametros(new TecnicoParameters()
                {
                    Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                    CodTecnicos = parameters.CodTecnicos
                });

                usuarios.AddRange(tecnicos.Select(u => u.Usuario));
            }
            else if (parameters.CodTecnico.HasValue)
            {
                Tecnico tecnico = _tecnicoRepo.ObterPorCodigo(parameters.CodTecnico.Value);
                usuarios.Add(tecnico.Usuario);
            }

            var pontos = ObterPontosDoDia(parameters, usuarios);
            var agendasValidados = FormatarAgendas(agendas);
            agendasValidados.AddRange(pontos);

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
                AgendaTecnico intervalo = agendas.FirstOrDefault(i => i.CodTecnico == codTec && i.Tipo == 
                    AgendaTecnicoTypeEnum.INTERVALO && i.Inicio.Date == DateTime.Today.Date);
                AgendaTecnico fimExpediente = CriarFimDoExpediente(agendasValidados, codTec);

                if (fimExpediente != null) agendasValidados.Add(fimExpediente);
                if (intervalo != null) agendasValidados.Add(intervalo);
            }

            return agendasValidados.Distinct().ToArray();
        }

        private List<AgendaTecnico> FormatarAgendas(List<AgendaTecnico> agendas)
        {
            List<AgendaTecnico> eventosValidados = new();
            List<AgendaTecnico> eventosParaAtualizar = new();
            var agendasAgrupadas = agendas.GroupBy(i => i.CodTecnico).ToList();

            foreach (var ags in agendasAgrupadas)
            {
                foreach (var agendaDoTecnico in ags)
                {
                    var isAgendado = contemAgendamento(agendaDoTecnico);

                    if (agendaDoTecnico.OrdemServico?.CodStatusServico == (int)StatusServicoEnum.FECHADO && agendaDoTecnico.Tipo == AgendaTecnicoTypeEnum.OS)
                    {
                        agendaDoTecnico.Cor = GetStatusColor((StatusServicoEnum)agendaDoTecnico.OrdemServico.CodStatusServico);
                        eventosValidados.Add(agendaDoTecnico);
                    }
                    else if (isAgendado && agendaDoTecnico.Tipo == AgendaTecnicoTypeEnum.OS)
                    {
                        if (agendaDoTecnico.OrdemServico.CodStatusServico == (int)StatusServicoEnum.CANCELADO || 
                            agendaDoTecnico.OrdemServico.CodStatusServico == (int)StatusServicoEnum.ABERTO)
                        {
                            agendaDoTecnico.IndAtivo = 0;
                            agendaDoTecnico.CodUsuarioManut = Constants.SISTEMA_NOME;
                            agendaDoTecnico.DataHoraManut = DateTime.Now;
                            eventosParaAtualizar.Add(agendaDoTecnico);
                        }
                        else
                        {
                            var agendamento = (DateTime)agendaDoTecnico.OrdemServico?.Agendamentos?
                                .OrderByDescending(a => a.DataAgendamento)
                                .FirstOrDefault().DataAgendamento;

                            agendaDoTecnico.Cor = AgendamentoColor;
                            agendaDoTecnico.Inicio = agendamento;
                            agendaDoTecnico.Fim = agendamento.AddHours(1);
                            agendaDoTecnico.IndAgendamento = 1;
                            eventosParaAtualizar.Add(agendaDoTecnico);
                        }
                        
                        eventosValidados.Add(agendaDoTecnico);
                    }
                    else if (!isAgendado && agendaDoTecnico.Tipo == AgendaTecnicoTypeEnum.OS)
                    {
                        if (agendaDoTecnico.Fim < DateTime.Now && agendaDoTecnico.Tipo == AgendaTecnicoTypeEnum.OS)
                        {
                            if (agendaDoTecnico.OrdemServico.CodStatusServico == (int)StatusServicoEnum.CANCELADO || 
                                agendaDoTecnico.OrdemServico.CodStatusServico == (int)StatusServicoEnum.ABERTO) 
                            {
                                agendaDoTecnico.IndAtivo = 0;
                            }

                            agendaDoTecnico.Cor = GetStatusColor((StatusServicoEnum)agendaDoTecnico.OrdemServico.CodStatusServico);
                            agendaDoTecnico.CodUsuarioManut = Constants.SISTEMA_NOME;
                            agendaDoTecnico.DataHoraManut = DateTime.Now;
                            eventosParaAtualizar.Add(agendaDoTecnico);
                            eventosValidados.Add(agendaDoTecnico);
                        }
                        else {
                            eventosValidados.Add(agendaDoTecnico);
                        }
                    }
                    else if (isAgendado && agendaDoTecnico.Tipo == AgendaTecnicoTypeEnum.OS &&
                             agendaDoTecnico.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO &&
                             agendaDoTecnico.Fim.Date < DateTime.Now.Date)
                    {
                        var eventosRealocados = ExcluirEventosComAtraso(ags.ToList());
                        eventosValidados.AddRange(eventosRealocados);
                    }
                }
            }

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
                    eventosParaAtualizar.Add(a);
                });
            });

            _agendaRepo.AtualizarListaAsync(eventosParaAtualizar);
            return eventosValidados.Where(i => i.IndAtivo == 1).ToList();
        }

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, string codTecnicos) =>
           _agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnicos = codTecnicos,
               IndAtivo = 1
           }).ToList();

        private List<AgendaTecnico> ObterAgenda(DateTime inicioPeriodo, DateTime fimPeriodo, int? codTecnico) =>
           _agendaRepo.ObterQuery(new AgendaTecnicoParameters
           {
               InicioPeriodoAgenda = inicioPeriodo,
               FimPeriodoAgenda = fimPeriodo,
               CodTecnico = codTecnico,
               IndAtivo = 1
           }).ToList();

        private AgendaTecnico CriarFimDoExpediente(List<AgendaTecnico> agendas, int codTecnico)
        {
            var primeiroPontoDoDia =
                agendas
                .Where(i => i.CodTecnico == codTecnico && i.Tipo == AgendaTecnicoTypeEnum.PONTO && i.Inicio.Date == DateTime.Today.Date).OrderBy(i => i.Inicio).FirstOrDefault();

            if (primeiroPontoDoDia == null) return null;

            AgendaTecnico fimExpediente = new AgendaTecnico
            {
                CodTecnico = codTecnico,
                CodUsuarioCad = Constants.SISTEMA_NOME,
                DataHoraCad = DateTime.Now,
                Cor = GetTypeColor(AgendaTecnicoTypeEnum.FIM_EXPEDIENTE),
                Tipo = AgendaTecnicoTypeEnum.FIM_EXPEDIENTE,
                Titulo = FimExpedienteTitle,
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
                    var pontosUsuario = _pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
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
                            Cor = GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                            Tipo = AgendaTecnicoTypeEnum.PONTO,
                            Titulo = PontoTitle,
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
                var pontosUsuario = _pontoUsuarioRepo.ObterPorParametros(new PontoUsuarioParameters
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
                            Cor = GetTypeColor(AgendaTecnicoTypeEnum.PONTO),
                            Titulo = PontoTitle,
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
                        e.Cor = GetStatusColor((StatusServicoEnum)e.OrdemServico.CodStatusServico);
                        atualizarAgendas.Add(e);
                    }
                    else if (e.Inicio.Date < DateTime.Now.Date && e.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO)
                    {
                        e.CodUsuarioManut = Constants.SISTEMA_NOME;
                        e.DataHoraManut = DateTime.Now;
                        e.IndAtivo =  0;
                        _agendaRepo.Atualizar(e);
                        e.OrdemServico.CodStatusServico = (int)StatusServicoEnum.ABERTO;
                        e.OrdemServico.CodUsuarioManut = Constants.SISTEMA_NOME;
                        e.OrdemServico.DataHoraManut = DateTime.Now;
                        _osRepo.Atualizar(e.OrdemServico);

                        _emailService.Enviar(new Email() {
                            Assunto = "Exclusão de Chamado da Agenda",
                            EmailDestinatario = "equipe.sat@perto.com.br",
                            EmailRemetente = "equipe.sat@perto.com.br",
                            Corpo = e.OrdemServico.CodOS.ToString() + ": " + e.OrdemServico.StatusServico.NomeStatusServico,
                            NomeDestinatario = "SAT",
                            NomeRemetente = "SAT"
                        });
                    }
                });

            _agendaRepo.AtualizarListaAsync(atualizarAgendas);
            return agendasTecnico.Where(i => i.IndAtivo == 1).ToList();
        }
    }
}