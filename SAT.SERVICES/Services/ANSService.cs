using System;
using System.Collections.Generic;
using System.Linq;
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
            int horas = 0;

            DateTime inicio = chamado.DataHoraCad.Value;
            DateTime fim = chamado.RelatoriosAtendimento
                .OrderByDescending(r => r.DataHoraSolucao)
                .FirstOrDefault()
                .DataHoraSolucao;

            var ans = new ANS
            {
                
            };

            // primeiro agendamento
            DateTime agendamento = chamado.Agendamentos
                .OrderBy(a => a.DataAgendamento)
                .FirstOrDefault()
                .DataAgendamento
                .Value;

            if (ans.PermiteAgendamento == Constants.SIM && agendamento != default(DateTime))
                inicio = agendamento;

            // Feriados
            var feriados = (IEnumerable<Feriado>)_feriadoService.ObterPorParametros(new FeriadoParameters
            {
                dataInicio = inicio,
                dataFim = fim,
                CodCidades = chamado.LocalAtendimento.CodCidade.ToString()
            }).Items;

            // Loop principal, acrescenta um minuto por iteracao
            for (var i = inicio; i < fim; i = i.AddMinutes(1))
            {
                var isFeriado = feriados
                    .Where(f => inicio.Date >= f.Data.Value.Date && fim.Date <= f.Data.Value.Date) is not null;

                if (isFeriado && ans.Feriado == Constants.NAO)
                    continue;

                if (i.DayOfWeek != DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                    continue;

                if (i.DayOfWeek != DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                    continue;

                if (i.TimeOfDay <= ans.HoraInicio)
                    continue;

                if (i.TimeOfDay >= ans.HoraFim)
                    continue;

                horas++;
            }

            return inicio.AddHours(horas);
        }
    }
}
