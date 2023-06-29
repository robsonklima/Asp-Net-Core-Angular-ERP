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
            DateTime previsao = chamado.DataHoraAberturaOS.Value;

            if (ans is null) return null;

            var agendamento = chamado.Agendamentos
                .OrderBy(a => a.DataAgendamento)
                .FirstOrDefault();

            if (ans.PermiteAgendamento == Constants.SIM && agendamento is not null)
                previsao = agendamento.DataAgendamento.Value;

            var feriados = (IEnumerable<SATFeriado>)_feriadoService
                .ObterPorParametros(new SATFeriadoParameters
                {
                    Mes = chamado.DataHoraAberturaOS.Value.Month
                })
                .Items;

            for (int i = 0; i < ans.TempoHoras;)
            {
                previsao = AplicarHorarioNaoUtil(previsao, ans);
                previsao = AplicarHorarioUtil(previsao, ans);
                previsao = AplicarFeriados(feriados, chamado, ans, previsao);
                previsao = previsao.AddHours(1);
                i++;
            }

            if (ans.ArredondaHoraFinal == Constants.SIM)
                previsao = previsao.Date + new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);

            return previsao;
        }

        private DateTime AplicarHorarioNaoUtil(DateTime previsao, ANS ans)
        {
            if (previsao.DayOfWeek == DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                return PularDia(previsao, ans);

            if (previsao.DayOfWeek == DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                return PularDia(previsao, ans);

            return previsao;
        }

        private DateTime AplicarHorarioUtil(DateTime previsao, ANS ans)
        {
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
                string uf = chamado.LocalAtendimento.Cidade.UnidadeFederativa.SiglaUF;
                string cidade = StringHelper.RemoverAcentos(chamado.LocalAtendimento.Cidade.NomeCidade);

                if (f.Tipo == FeriadoTipoConst.NACIONAL && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.ESTADUAL && f.UF == uf && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.FACULTATIVO && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans);
                
                if (f.Tipo == FeriadoTipoConst.MUNICIPAL && f.Municipio == cidade && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
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
