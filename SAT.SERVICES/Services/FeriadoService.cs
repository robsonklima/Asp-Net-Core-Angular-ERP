using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FeriadoService : IFeriadoService
    {
        private readonly IFeriadoRepository _feriadoRepo;

        public FeriadoService(IFeriadoRepository feriadoRepo)
        {
            _feriadoRepo = feriadoRepo;
        }

        public ListViewModel ObterPorParametros(FeriadoParameters parameters)
        {
            var feriados = _feriadoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = feriados,
                TotalCount = feriados.TotalCount,
                CurrentPage = feriados.CurrentPage,
                PageSize = feriados.PageSize,
                TotalPages = feriados.TotalPages,
                HasNext = feriados.HasNext,
                HasPrevious = feriados.HasPrevious
            };

            return lista;
        }

        public Feriado Criar(Feriado feriado)
        {
            _feriadoRepo.Criar(feriado);
            return feriado;
        }

        public void Deletar(int codigo)
        {
            _feriadoRepo.Deletar(codigo);
        }

        public void Atualizar(Feriado feriado)
        {
            _feriadoRepo.Atualizar(feriado);
        }

        public Feriado ObterPorCodigo(int codigo)
        {
            return _feriadoRepo.ObterPorCodigo(codigo);
        }

        /// <summary>
        ///  Retorna os feriados do mês de acordo com uf/cidade (em dias)
        /// </summary>
        public int ObterNroFeriadosDoPeriodo(DateTime dataInicial, DateTime dataFinal, int? codCidade, int? codUF, IEnumerable<Feriado> feriados)
        {
            IEnumerable<Feriado> feriadosBasePascoa, feriadosNacionais,
                        feriadosMunicipais = Enumerable.Empty<Feriado>(), feriadosEstaduais = Enumerable.Empty<Feriado>();

            if (codCidade.HasValue)
                feriadosMunicipais = feriados.Where(f => !f.QtdeDias.HasValue && f.Data.Value.Year != 1900 &&
                    f.Data >= dataInicial && f.Data <= dataFinal && f.CodCidade.HasValue && f.CodCidade.Value == codCidade.Value);

            if (codUF.HasValue)
                feriadosEstaduais = feriados.Where(f => !f.CodCidade.HasValue && f.Data.Value.Year != 1900 &&
                    f.Data >= dataInicial && f.Data <= dataFinal && f.CodUF.HasValue && f.CodUF.Value == codUF.Value);

            feriadosBasePascoa = feriados.Where(f => f.QtdeDias.HasValue).ToList().Select(f =>
            {
                var dataFeriado = DateTimeEx.CalcHolidays(dataInicial.Year, (FeriadoEnum)f.CodFeriado);

                if (dataFeriado.HasValue && dataFeriado >= dataInicial && dataFeriado < dataFinal)
                {
                    f.Data = DateTimeEx.SetDate(dataFeriado);
                    return f;
                }
                return null;
            });

            feriadosNacionais = feriados.Where(f => !f.CodUF.HasValue && !f.CodCidade.HasValue && !f.QtdeDias.HasValue &&
             f.Data.Value.Year == 1900).ToList().Select(f =>
             {
                 var dataFeriado = new DateTime(dataInicial.Year, f.Data.Value.Month, f.Data.Value.Day);
                 return dataFeriado >= dataInicial && dataFeriado < dataFinal ? f : null;
             });

            var uniaoFeriados = feriadosBasePascoa.Union(feriadosNacionais)
                                                  .Union(feriadosEstaduais)
                                                  .Union(feriadosMunicipais);

            uniaoFeriados.Where(f => f != null)
                         .ToList()
                         .ForEach(r => r.Data =
                                new DateTime(dataInicial.Year, r.Data.Value.Month, r.Data.Value.Day));

            var datasFeriados = uniaoFeriados.Where(f => f != null)
                                             .Select(u => u.Data)
                                             .Distinct()
                                             .Where(d => d.Value.DayOfWeek != DayOfWeek.Saturday &&
                                                                d.Value.DayOfWeek != DayOfWeek.Sunday);

            return datasFeriados.Count();
        }

        public IEnumerable<Feriado> ObterFeriadosDoPeriodo(DateTime data) =>
         _feriadoRepo.ObterPorParametros(new FeriadoParameters
         {
             Mes = data,
             PageSize = Int32.MaxValue
         }).Where(f => f.Data.HasValue);

        public int GetDiasUteis(DateTime dataInicio, DateTime dataFim)
        {
            int totalDias = (int)dataFim.Subtract(dataInicio).TotalDays;
            return totalDias - this.CalculaDiasNaoUteis(dataInicio, dataFim);
        }

        public int CalculaDiasNaoUteis(DateTime dataInicio, DateTime dataFim, bool contabilizarSabado = false, bool contabilizarDomingo = false, bool contabilizarFeriados = false, int? codCidade = null)
        {
            return this._feriadoRepo.CalculaDiasNaoUteis(dataInicio, dataFim, contabilizarSabado, contabilizarDomingo, contabilizarFeriados, codCidade);
        }
    }
}
