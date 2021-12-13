using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public AgendaTecnico CriarAgendaTecnico(int codOS, int codTecnico)
        {
            var os = this._osRepo.ObterEntidadePorCodigo(codOS);

            var inicioPeriodo = DateTimeEx.FirstDayOfWeek(DateTime.Now);
            var fimPeriodo = DateTimeEx.LastDayOfWeek(inicioPeriodo);

            var agendamentos = this.ObterAgenda(inicioPeriodo, fimPeriodo, codTecnico);
            var ag = this.CriaNovoEventoOS(agendamentos, os, 60, codTecnico);

            return ag;
        }

        private AgendaTecnico CriaNovoEventoOS(List<AgendaTecnico> agendamentos, OrdemServico os, int mediaTecnico, int codTecnico)
        {
            if (os.Agendamentos != null && os.Agendamentos.Any())
                return CriaNovoEventoOSComAgendamento(agendamentos, os, mediaTecnico, codTecnico);

            var ultimoEvento = agendamentos
                .Where(i => i.CodTecnico == codTecnico && i.Tipo == AgendaTecnicoTypeEnum.OS)
              .OrderByDescending(i => i.Fim)
              .FirstOrDefault();

            var deslocamento = this.DistanciaEmMinutos(os, ultimoEvento?.OrdemServico);

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
                CodUsuarioCad = "ADMIN",
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

            var eventosSobrepostos =
                agendasTecnico.Where(i => i.CodOS != os.CodOS && i.CodOS > 0 && ((start >= i.Inicio && i.Fim <= end) || (i.Inicio >= start && end <= i.Fim)));

            if (agendaTecnicoAnterior != null)
            {
                agendaTecnicoAnterior.Inicio = start;
                agendaTecnicoAnterior.Fim = end;
                agendaTecnicoAnterior.Cor = this.IsAgendamentoColor;
                agendaTecnicoAnterior.DataHoraManut = DateTime.Now;
                agendaTecnicoAnterior.IndAgendamento = 1;
                agendaTecnicoAnterior.CodUsuarioManut = "ADMIN";

                var ag = this._agendaRepo.Atualizar(agendaTecnicoAnterior);
                agendasTecnico.Add(ag);
                this.RealocarEventosSobrepostos(agendasTecnico, eventosSobrepostos);

                return agendaTecnicoAnterior;
            }
            else
            {
                AgendaTecnico agendaTecnico = new AgendaTecnico
                {
                    Inicio = start,
                    Fim = end,
                    Titulo = os.LocalAtendimento?.NomeLocal?.ToUpper(),
                    Cor = this.IsAgendamentoColor,
                    CodOS = os.CodOS,
                    CodTecnico = codTecnico,
                    Tipo = AgendaTecnicoTypeEnum.OS,
                    IndAgendamento = 1,
                    IndAtivo = 1,
                    CodUsuarioCad = "ADMIN",
                    DataHoraCad = DateTime.Now
                };

                var ag = this._agendaRepo.Criar(agendaTecnico);
                agendasTecnico.Add(ag);
                this.RealocarEventosSobrepostos(agendasTecnico, eventosSobrepostos);

                return agendaTecnico;
            }
        }

        private void RealocarEventosSobrepostos(List<AgendaTecnico> agendasTecnico, IEnumerable<AgendaTecnico> eventosSobrepostos)
        {
            if (!eventosSobrepostos.Any()) return;

            var ultimoEvento = agendasTecnico.OrderByDescending(i => i.Fim).FirstOrDefault();
            var ultimaOS = ultimoEvento.OrdemServico;

            eventosSobrepostos.ToList().ForEach(e =>
            {
                var os = e.OrdemServico;
                var deslocamento = this.DistanciaEmMinutos(os, ultimaOS);

                var start = ultimoEvento != null ? ultimoEvento.Fim : this.InicioExpediente;

                // se começa durante a sugestão de intervalo
                if (this.isIntervalo(start))
                    start = this.InicioIntervalo;
                else if (start >= this.FimExpediente)
                {
                    start = start.AddDays(1);
                    if (this.isIntervalo(start))
                        start = new DateTime(start.Year, start.Month, start.Day, this.FimIntervalo.Hour, this.FimIntervalo.Minute, 0);
                }

                var duracao = (e.Fim - e.Inicio).TotalMinutes;

                // adiciona deslocamento
                start = start.AddMinutes(deslocamento);

                // se termina durante a sugestao de intervalo
                var end = start.AddMinutes(duracao);
                if (this.isIntervalo(end))
                {
                    start = end.AddMinutes(deslocamento);
                    end = start.AddMinutes(duracao);
                }

                e.Inicio = start;
                e.Fim = end;
                e.CodUsuarioManut = "ADMIN";
                e.DataHoraManut = DateTime.Now;
                var ag = this._agendaRepo.Atualizar(e);

                ultimoEvento = ag;
                ultimaOS = os;
            });
        }

        private double DistanciaEmMinutos(OrdemServico osAtual, OrdemServico osAnterior)
        {
            double? orig_lat = null, orig_long = null, dest_lat = null, dest_long = null;

            if (osAnterior != null)
            {
                if (!string.IsNullOrEmpty(osAnterior.LocalAtendimento.Latitude)) orig_lat = double.Parse(osAnterior.LocalAtendimento.Latitude, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(osAnterior.LocalAtendimento.Longitude)) orig_long = double.Parse(osAnterior.LocalAtendimento.Longitude, CultureInfo.InvariantCulture);
            }
            else if (osAtual.Tecnico != null)
            {
                if (!string.IsNullOrEmpty(osAtual.Tecnico.Latitude)) orig_lat = double.Parse(osAtual.Tecnico.Latitude, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(osAtual.Tecnico.Longitude)) orig_long = double.Parse(osAtual.Tecnico.Longitude, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(osAtual.LocalAtendimento.Latitude)) dest_lat = double.Parse(osAtual.LocalAtendimento.Latitude, CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(osAtual.LocalAtendimento.Longitude)) dest_long = double.Parse(osAtual.LocalAtendimento.Longitude, CultureInfo.InvariantCulture);

            return this.GetDistanceInMinutesPerKm(orig_lat, orig_long, dest_lat, dest_long, 50);
        }
        private string GetTypeColor(AgendaTecnicoTypeEnum type)
        {
            switch (type)
            {
                case AgendaTecnicoTypeEnum.OS:
                    return "#009000";
                case AgendaTecnicoTypeEnum.PONTO:
                    return "#C8C8C8C8";
                case AgendaTecnicoTypeEnum.INTERVALO:
                    return "#C8C8C8C8";
                default:
                    return "#C8C8C8C8";
            }
        }

        private string IsAgendamentoColor => "#381354";
        private bool isIntervalo(DateTime time) => time >= InicioIntervalo && time <= FimIntervalo;
        private DateTime InicioExpediente => DateTime.Now.Date.Add(new TimeSpan(8, 00, 0));
        private DateTime FimExpediente => DateTime.Now.Date.Add(new TimeSpan(18, 00, 0));
        private DateTime InicioIntervalo => DateTime.Now.Date.Add(new TimeSpan(12, 00, 0));
        private DateTime FimIntervalo => DateTime.Now.Date.Add(new TimeSpan(13, 00, 0));
    }
}