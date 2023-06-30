using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        public DateTime? CalcularPrazo(OrdemServico chamado)
        {
            var ans = chamado?.EquipamentoContrato?.ANS;
            DateTime previsao = chamado.DataHoraCad ?? chamado.DataHoraAberturaOS.Value;

            if (ans is null) return null;

            previsao = AplicarAgendamentos(chamado, ans, previsao);

            var feriados = (dynamic)_feriadoService
                .ObterPorParametros(new SATFeriadoParameters{ Mes = previsao.Month })
                .Items;

            for (int i = 0; i < ans.TempoHoras; i++)
            {
                previsao = AplicarJanelaHorarios(previsao, ans);
                previsao = AplicarFeriados(feriados, chamado, ans, previsao);
                previsao = previsao.AddHours(1);
            }

            if (ans.ArredondaHoraFinal == Constants.SIM)
                previsao = previsao.Date + new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);

            return previsao;
        }

        private DateTime AplicarAgendamentos(OrdemServico chamado, ANS ans, DateTime previsao)
        {
            var agendamentos = chamado.Agendamentos
                .OrderByDescending(a => a.DataAgendamento);

            if (ans.PermiteAgendamento == Constants.SIM && ans.PermiteAgendamentoAtePrimeiraRAT == Constants.SIM)
            {
                var dataAtendimento = chamado.RelatoriosAtendimento
                    .OrderByDescending(r => r.DataHoraInicio)
                    .FirstOrDefault().DataHoraInicio;

                var agendamento = agendamentos
                    .Where(a => a.DataAgendamento <= dataAtendimento)
                    .OrderByDescending(a => a.DataAgendamento)
                    .FirstOrDefault();

                return agendamento.DataAgendamento.Value;
            }

            if (ans.PermiteAgendamento == Constants.SIM && ans.PermiteAgendamentoAtePrimeiraRAT == Constants.NAO)
            {
                var agendamento = agendamentos
                    .OrderByDescending(a => a.DataAgendamento)
                    .FirstOrDefault();
            }

            return previsao;
        }

        private DateTime AplicarJanelaHorarios(DateTime previsao, ANS ans)
        {
         
            if (previsao.DayOfWeek == DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                return PularDia(previsao, ans);

            if (previsao.DayOfWeek == DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                return PularDia(previsao, ans);

            if (previsao.TimeOfDay < ans.HoraInicio)
                return PularDia(previsao, ans);

            if (previsao.TimeOfDay > ans.HoraFim)
                return PularDia(previsao, ans);

            return previsao;
        }

        private DateTime AplicarFeriados(IEnumerable<SATFeriado> feriados, OrdemServico chamado, ANS ans, DateTime previsao)
        {
            var feriadosDoDia = feriados.Where(f => f.Data.Date == previsao.Date);

            foreach (var f in feriadosDoDia)
            {
                string u = chamado.LocalAtendimento.Cidade.UnidadeFederativa.SiglaUF;
                string c = StringHelper.RemoverAcentos(chamado.LocalAtendimento.Cidade.NomeCidade);

                if (f.Tipo == FeriadoTipoConst.NACIONAL && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.ESTADUAL && f.UF == u && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.FACULTATIVO && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.MUNICIPAL && f.Municipio == c && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
            }

            return previsao;
        }

        private DateTime PularDia(DateTime previsao, ANS ans)
        {
            var hrInicioPrevisao = new TimeSpan(previsao.TimeOfDay.Hours, previsao.TimeOfDay.Minutes, previsao.TimeOfDay.Seconds);
            var hrInicioANS = new TimeSpan(ans.HoraInicio.Hours, ans.HoraInicio.Minutes, ans.HoraInicio.Seconds);
            var hrFimANS = new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);
            var saldo = previsao.TimeOfDay - hrFimANS;
            var novaData = (previsao.AddDays(1).Date + hrInicioANS);
            var novoPrevisao = novaData.AddMilliseconds(saldo.Milliseconds);

            return novoPrevisao;
        }
    }
}
