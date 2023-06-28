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
            DateTime inicio = chamado.DataHoraCad.Value;
            DateTime fim = chamado.DataHoraFechamento.Value;
            var ans = new ANS {};
            var primeiroAgendamento = chamado.Agendamentos
                .OrderBy(a => a.DataAgendamento)
                .FirstOrDefault();
            
            int horas = 0;

            if (ans.PermiteAgendamento == Constants.SIM)
                if (primeiroAgendamento.DataAgendamento.Value != default(DateTime))
                    inicio = primeiroAgendamento.DataAgendamento.Value;

            var parameters = new FeriadoParameters {
                dataInicio = inicio,
                dataFim = fim
            };
            var feriados = (IEnumerable<Feriado>)_feriadoService.ObterPorParametros(parameters).Items;

            for (var i = inicio; i < fim; i = i.AddHours(1))
            {
                foreach (var feriado in feriados)
                    if (inicio.Date >= feriado.Data.Value.Date && fim.Date <= feriado.Data.Value.Date)
                        continue;

                if (i.DayOfWeek != DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                    continue;

                if (i.DayOfWeek != DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                    continue;

                if (i.TimeOfDay.Hours <= ans.HoraInicio)
                    continue;

                if (i.TimeOfDay.Hours < ans.HoraFim)
                    continue;

                horas++;
            }

            return inicio.AddHours(horas);
        }
    }
}
