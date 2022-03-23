using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public AgendaTecnico CriarAgendaTecnico(int codOS, int codTecnico)
        {
            var os = this._osRepo.ObterPorCodigo(codOS);

            this.DeletarAgendaTecnico(codOS);

            var inicioPeriodo = DateTimeEx.FirstDayOfWeek(DateTime.Now);
            var fimPeriodo = DateTimeEx.LastDayOfWeek(inicioPeriodo);
            var agendas = this.ObterAgenda(inicioPeriodo, fimPeriodo, codTecnico);

            var ag = this.CriaNovoEventoOS(agendas, os, 60, codTecnico);
            return ag;
        }

        private AgendaTecnico CriaNovoEventoOS(List<AgendaTecnico> agendas, OrdemServico os, int mediaTecnico, int codTecnico)
        {
            if (os.Agendamentos != null && os.Agendamentos.Any())
                return CriaNovoEventoOSComAgendamento(agendas, os, mediaTecnico, codTecnico);

            var ultimaAgenda = agendas
                .Where(
                    i => i.CodTecnico == codTecnico &&
                    i.Tipo == AgendaTecnicoTypeEnum.OS &&
                    i.IndAgendamento == 0 &&
                    i.Inicio.Date == DateTime.Now.Date
                )
                .OrderByDescending(i => i.Fim)
                .FirstOrDefault();

            var deslocamento = this.DistanciaEmMinutos(os, ultimaAgenda?.OrdemServico);

            var start = ultimaAgenda != null ? ultimaAgenda.Fim : this.InicioExpediente();

            // adiciona deslocamento
            start = start.AddMinutes(deslocamento);
            if (this.isIntervalo(start))
                start = this.FimIntervalo(start);

            // se termina durante a sugestao de intervalo
            var end = start.AddMinutes(mediaTecnico);
            if (this.isIntervalo(end))
            {
                start = end.AddMinutes(deslocamento);
                end = start.AddMinutes(mediaTecnico);
            }

            AgendaTecnico agendaTecnico = new AgendaTecnico
            {
                Inicio = start,
                Fim = end,
                Titulo = os.LocalAtendimento?.NomeLocal?.ToUpper(),
                Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.OS),
                CodOS = os.CodOS,
                CodTecnico = codTecnico,
                Tipo = AgendaTecnicoTypeEnum.OS,
                IndAgendamento = 0,
                IndAtivo = 1,
                CodUsuarioCad = Constants.SISTEMA_NOME,
                DataHoraCad = DateTime.Now
            };

            var ag = this._agendaRepo.Criar(agendaTecnico);
            return ag;
        }

        private AgendaTecnico CriaNovoEventoOSComAgendamento(List<AgendaTecnico> agendasTecnico, OrdemServico os, int mediaTecnico, int codTecnico)
        {
            var start = os.Agendamentos.OrderByDescending(i => i.CodAgendamento).FirstOrDefault().DataAgendamento.Value;
            var end = start.AddMinutes(mediaTecnico);

            var agendaTecnicoAnterior =
                agendasTecnico.FirstOrDefault(i => i.CodOS == os.CodOS);

            // var eventosSobrepostos =
            //     agendasTecnico.Where(i => i.Tipo == AgendaTecnicoTypeEnum.OS && i.IndAgendamento == 0 && ((start >= i.Inicio && i.Fim <= end) || (i.Inicio >= end)));

            if (agendaTecnicoAnterior != null)
            {
                agendaTecnicoAnterior.Inicio = start;
                agendaTecnicoAnterior.Fim = end;
                agendaTecnicoAnterior.Cor = this.AgendamentoColor;
                agendaTecnicoAnterior.DataHoraManut = DateTime.Now;
                agendaTecnicoAnterior.IndAgendamento = 1;
                agendaTecnicoAnterior.CodUsuarioManut = Constants.SISTEMA_NOME;

                var ag = this._agendaRepo.Atualizar(agendaTecnicoAnterior);
                // this.RealocarEventosSobrepostos(ag, eventosSobrepostos);

                return agendaTecnicoAnterior;
            }
            else
            {
                AgendaTecnico agendaTecnico = new AgendaTecnico
                {
                    Inicio = start,
                    Fim = end,
                    Titulo = os.LocalAtendimento?.NomeLocal?.ToUpper(),
                    Cor = this.AgendamentoColor,
                    CodOS = os.CodOS,
                    CodTecnico = codTecnico,
                    Tipo = AgendaTecnicoTypeEnum.OS,
                    IndAgendamento = 1,
                    IndAtivo = 1,
                    CodUsuarioCad = Constants.SISTEMA_NOME,
                    DataHoraCad = DateTime.Now
                };

                var ag = this._agendaRepo.Criar(agendaTecnico);

                // this.RealocarEventosSobrepostos(ag, eventosSobrepostos);

                return agendaTecnico;
            }
        }

        private void RealocarEventosSobrepostos(AgendaTecnico ag, IEnumerable<AgendaTecnico> eventosSobrepostos)
        {
            if (!eventosSobrepostos.Any()) return;

            var ultimoEvento = ag;
            var ultimaOS = ag.OrdemServico != null ? ultimoEvento.OrdemServico : this._osRepo.ObterPorCodigo(ag.CodOS.Value);

            eventosSobrepostos.OrderBy(i => i.Inicio).ToList().ForEach(e =>
            {
                var os = e.OrdemServico;
                var deslocamento = this.DistanciaEmMinutos(os, ultimaOS);

                var start = ultimoEvento != null ? ultimoEvento.Fim : this.InicioExpediente();
                var duracao = (e.Fim - e.Inicio).TotalMinutes;

                // adiciona deslocamento
                start = start.AddMinutes(deslocamento);

                // se começa durante o intervalo ou depois do expediente
                if (this.isIntervalo(start))
                    start = this.FimIntervalo(start);

                // se termina durante a sugestao de intervalo
                var end = start.AddMinutes(duracao);
                if (this.isIntervalo(end))
                {
                    start = end.AddMinutes(deslocamento);
                    end = start.AddMinutes(duracao);
                }

                e.Inicio = start;
                e.Fim = end;
                e.CodUsuarioManut = Constants.SISTEMA_NOME;
                e.DataHoraManut = DateTime.Now;
                var ag = this._agendaRepo.Atualizar(e);

                ultimoEvento = ag;
                ultimaOS = os;
            });
        }
    }
}