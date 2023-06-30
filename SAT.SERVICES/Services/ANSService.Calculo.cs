using System;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        public DateTime? CalcularPrazo(int codOS)
        {
            var os = (dynamic)ObterOS(codOS);

            if (os is null) return null;

            var ans = os.EquipamentoContrato.ANS;
            DateTime previsao = os.DataHoraCad ?? os.DataHoraAberturaOS;
            previsao = AplicarAgendamentos(os, ans, previsao);

            for (int i = 0; i < ans.TempoHoras; i++)
            {
                previsao = AplicarJanelaHorarios(previsao, ans, i);
                previsao = AplicarFeriados(os.LocalAtendimento.Cidade, ans, previsao, i);
                previsao = previsao.AddHours(1);
            }

            if (ans.ArredondaHoraFinal == Constants.SIM)
                previsao = previsao.Date + new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);

            return previsao;
        }

        private OrdemServico ObterOS(int codOS)
        {
            var os = (dynamic)_ordemServicoService
                .ObterPorParametros(new OrdemServicoParameters
                {
                    CodOS = codOS.ToString(),
                    Include = OrdemServicoIncludeEnum.ANS
                })
                .Items
                .FirstOrDefault();

            if (os.EquipamentoContrato is null)
            {
                _logger.Info($"Chamado { os.CodOS } nao possui Equipamento");

                return null;
            }

            if (os.EquipamentoContrato.ANS is null)
            {
                _logger.Info($"Chamado { os.CodOS } nao possui SLA");

                return null;
            }

            if (os.LocalAtendimento is null)
            {
                _logger.Info($"Chamado { os.CodOS } nao possui local");

                return null;
            }

            if (os.LocalAtendimento.Cidade is null)
            {
                _logger.Info($"Chamado { os.CodOS } nao possui cidade");

                return null;
            }

            if (os.LocalAtendimento.Cidade.UnidadeFederativa is null)
            {
                _logger.Info($"Chamado { os.CodOS } nao possui UF");

                return null;
            }

            return os;
        }

        private DateTime AplicarAgendamentos(OrdemServico os, ANS ans, DateTime previsao)
        {
            if (ans.PermiteAgendamento == Constants.SIM)
            {
                var dataAtendimento = os.RelatoriosAtendimento
                    .OrderByDescending(r => r.DataHoraCad)
                    .FirstOrDefault()
                    .DataHoraCad;

                return (dynamic)os
                    .Agendamentos
                    .Where(a => a.DataHoraUsuAgendamento <= dataAtendimento)
                    .OrderByDescending(a => a.DataHoraUsuAgendamento)
                    .FirstOrDefault()
                    .DataAgendamento;
            }

            return previsao;
        }

        private DateTime AplicarJanelaHorarios(DateTime previsao, ANS ans, int iteracao)
        {
            if (ans.HorasUteis == Constants.NAO)
                return previsao;

            if (previsao.DayOfWeek == DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                return PularDia(previsao, ans, iteracao);

            if (previsao.DayOfWeek == DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                return PularDia(previsao, ans, iteracao);

            if (previsao.TimeOfDay < ans.HoraInicio)
                return PularDia(previsao, ans, iteracao);

            if (previsao.TimeOfDay > ans.HoraFim)
                return PularDia(previsao, ans, iteracao);

            return previsao;
        }

        private DateTime AplicarFeriados(Cidade cidade, ANS ans, DateTime previsao, int iteracao)
        {
            var feriados = (dynamic)_feriadoService
                .ObterPorParametros(new SATFeriadoParameters
                {
                    Mes = previsao.Month
                })
                .Items;

            foreach (var f in feriados)
            {
                string u = cidade.UnidadeFederativa.SiglaUF;
                string c = cidade.NomeCidade;

                if (f.Tipo == FeriadoTipoConst.NACIONAL && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans, iteracao);

                if (f.Tipo == FeriadoTipoConst.ESTADUAL && f.UF == u && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans, iteracao);

                if (f.Tipo == FeriadoTipoConst.FACULTATIVO && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans, iteracao);

                if (f.Tipo == FeriadoTipoConst.MUNICIPAL && f.Municipio == c && f.Data == previsao.Date && ans.Feriado == Constants.NAO)
                    return PularDia(previsao, ans, iteracao);
            }

            return previsao;
        }

        private DateTime PularDia(DateTime previsao, ANS ans, int iteracao)
        {
            var hrInicioPrevisao = new TimeSpan(previsao.TimeOfDay.Hours, previsao.TimeOfDay.Minutes, previsao.TimeOfDay.Seconds);
            var hrInicioANS = new TimeSpan(ans.HoraInicio.Hours, ans.HoraInicio.Minutes, ans.HoraInicio.Seconds);
            var hrFimANS = new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);
            var saldo = previsao.TimeOfDay - hrFimANS;
            var novaPrevisao = (previsao.AddDays(1).Date + hrInicioANS);

            if (iteracao != 0)
                novaPrevisao = novaPrevisao.AddMinutes(saldo.Minutes);

            return novaPrevisao;
        }
    }
}
