using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IANSRepository _ansRepo;
        private readonly IFeriadoService _feriadoService;

        public ANSService(
            IANSRepository ansRepo,
            IFeriadoService feriadoService
        )
        {
            _ansRepo = ansRepo;
            _feriadoService = feriadoService;
        }

        public ANS Atualizar(ANS ans)
        {
            return _ansRepo.Atualizar(ans);
        }

        public ANS Criar(ANS ans)
        {
            return _ansRepo.Criar(ans);
        }

        public ANS Deletar(int codigo)
        {
            return _ansRepo.Deletar(codigo);
        }

        public ANS ObterPorCodigo(int codigo)
        {
            return _ansRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(ANSParameters parameters)
        {
            var anss = _ansRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = anss,
                TotalCount = anss.TotalCount,
                CurrentPage = anss.CurrentPage,
                PageSize = anss.PageSize,
                TotalPages = anss.TotalPages,
                HasNext = anss.HasNext,
                HasPrevious = anss.HasPrevious
            };

            return lista;
        }

        public DateTime CalcularPrazo(OrdemServico chamado)
        {
            DateTime inicio = chamado.DataHoraAberturaOS.Value;
            DateTime prazo = inicio;

            var ans = chamado.EquipamentoContrato.ANS;

            if (ans is null)
            {
                _logger.Error($"{ MsgConst.ANS_NAO_LOCALIZADA }: { chamado.CodOS }");

                return DateTime.Now;
            }

            var agendamento = chamado.Agendamentos
                .OrderBy(a => a.DataAgendamento)
                .FirstOrDefault();

            if (ans.PermiteAgendamento == Constants.SIM && agendamento is not null)
            {
                inicio = agendamento.DataAgendamento.Value;
                prazo = inicio;
            }

            var feriadosNacionais = (IEnumerable<Feriado>)_feriadoService.ObterPorParametros(new FeriadoParameters
            {
                dataInicio = inicio,
                dataFim = chamado.DataHoraFechamento ?? DateTime.Now,
                FeriadosNacionais = true
            }).Items;

            var feriadosCidade = (IEnumerable<Feriado>)_feriadoService.ObterPorParametros(new FeriadoParameters
            {
                dataInicio = inicio,
                dataFim = chamado.DataHoraFechamento ?? DateTime.Now,
                CodCidades = chamado.LocalAtendimento.CodCidade.ToString()
            }).Items;

            for (int i = 0; i < ans.TempoMinutos;)
            {
                foreach (var feriado in feriadosNacionais)
                    if (feriado.Data.Value.Date == inicio.Date && ans.Feriado == Constants.NAO)
                        continue;

                foreach (var feriado in feriadosCidade)
                    if (feriado.Data.Value.Date == inicio.Date && ans.Feriado == Constants.NAO)
                        continue;

                if (prazo.DayOfWeek == DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                    continue;

                if (prazo.DayOfWeek == DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                    continue;

                if (prazo.TimeOfDay < ans.HoraInicio)
                {
                    var hrInicio = new TimeSpan(ans.HoraInicio.Hours, ans.HoraInicio.Minutes, ans.HoraInicio.Seconds);
                    prazo = prazo.Date + hrInicio;

                    continue;
                }

                if (prazo.TimeOfDay > ans.HoraFim)
                {
                    var hrInicio = new TimeSpan(ans.HoraInicio.Hours, ans.HoraInicio.Minutes, ans.HoraInicio.Seconds);
                    prazo = prazo.AddDays(1).Date + hrInicio;

                    continue;
                }

                prazo = prazo.AddMinutes(1);
                
                i++;
            }

            return prazo;
        }
    }
}
