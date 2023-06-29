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
using SAT.UTILS;

namespace SAT.SERVICES.Services
{
    public partial class ANSService : IANSService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IANSRepository _ansRepo;
        private readonly ISATFeriadoService _feriadoService;

        public ANSService(
            IANSRepository ansRepo,
            ISATFeriadoService feriadoService
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
            var ans = chamado.EquipamentoContrato.ANS;
            DateTime inicio = chamado.DataHoraAberturaOS.Value;
            DateTime prazo = inicio;

            if (ans is null)
            {
                _logger.Error($"{MsgConst.ANS_NAO_LOCALIZADA}: {chamado.CodOS}");

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

            IEnumerable<SATFeriado> feriados = (IEnumerable<SATFeriado>)_feriadoService
                .ObterPorParametros(new SATFeriadoParameters
                {
                    Mes = chamado.DataHoraAberturaOS.Value.Month
                })
                .Items;

            for (int i = 0; i < ans.TempoHoras;)
            {
                if (prazo.DayOfWeek == DayOfWeek.Saturday && ans.Sabado == Constants.NAO)
                {
                    prazo = AdicionarDia(prazo, ans);
                    continue;
                }

                if (prazo.DayOfWeek == DayOfWeek.Sunday && ans.Domingo == Constants.NAO)
                {
                    prazo = AdicionarDia(prazo, ans);
                    continue;
                }

                if (prazo.TimeOfDay < ans.HoraInicio)
                {
                    prazo = AdicionarDia(prazo, ans);
                    continue;
                }

                if (prazo.TimeOfDay > ans.HoraFim)
                {
                    prazo = AdicionarDia(prazo, ans);
                    continue;
                }

                foreach (var f in feriados.Where(f => f.Data.Date == prazo.Date)) 
                {
                    string uf = chamado.LocalAtendimento.Cidade.UnidadeFederativa.SiglaUF;
                    string cidade = StringHelper.RemoverAcentos(chamado.LocalAtendimento.Cidade.NomeCidade);
                    
                    if (f.Tipo == FeriadoTipoConst.NACIONAL && f.Data == inicio.Date && ans.Feriado == Constants.NAO)
                    {
                        prazo = AdicionarDia(prazo, ans);
                        continue;
                    }

                    if (f.Tipo == FeriadoTipoConst.ESTADUAL && f.UF == uf && f.Data == inicio.Date && ans.Feriado == Constants.NAO)
                    {
                        prazo = AdicionarDia(prazo, ans);
                        continue;
                    }

                    if (f.Tipo == FeriadoTipoConst.FACULTATIVO && f.Data == inicio.Date && ans.Feriado == Constants.NAO)
                    {
                        prazo = AdicionarDia(prazo, ans);
                        continue;
                    }

                    if (f.Tipo == FeriadoTipoConst.MUNICIPAL && f.Municipio == cidade && f.Data == inicio.Date && ans.Feriado == Constants.NAO)
                    {
                        prazo = AdicionarDia(prazo, ans);
                        continue;
                    }
                }

                prazo = prazo.AddHours(1);

                i++;
            }

            if (ans.ArredondaHoraFinal == Constants.SIM)
            {
                var hrFimANS = new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);

                prazo = prazo.Date + hrFimANS;
            }

            return prazo;
        }

        private DateTime AdicionarDia(DateTime prazo, ANS ans) 
        {
            var hrInicioPrazo = new TimeSpan(prazo.TimeOfDay.Hours, prazo.TimeOfDay.Minutes, prazo.TimeOfDay.Seconds);
            var hrInicioANS = new TimeSpan(ans.HoraInicio.Hours, ans.HoraInicio.Minutes, ans.HoraInicio.Seconds);
            var hrFimANS = new TimeSpan(ans.HoraFim.Hours, ans.HoraFim.Minutes, ans.HoraFim.Seconds);

            var diferenca = prazo.Minute - hrFimANS.Minutes;

            return (prazo.AddDays(1).Date + hrInicioANS).AddMinutes(diferenca);
        }
    }
}
