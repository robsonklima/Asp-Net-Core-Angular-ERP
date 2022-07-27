using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public class FeriadoRepository : IFeriadoRepository
    {
        private readonly AppDbContext _context;

        Feriado[] listaFeriados = null;

        public FeriadoRepository(AppDbContext context)
        {
            _context = context;
            this.listaFeriados = _context.Feriado.ToArray();
        }

        public void Atualizar(Feriado feriado)
        {
            _context.ChangeTracker.Clear();
            Feriado f = _context.Feriado.FirstOrDefault(f => f.CodFeriado == feriado.CodFeriado);

            if (f != null)
            {
                _context.Entry(f).CurrentValues.SetValues(feriado);
                _context.SaveChanges();
            }
        }

        public void Criar(Feriado feriado)
        {
            _context.Add(feriado);
            _context.SaveChanges();
        }

        public void Deletar(int codFeriado)
        {
            Feriado f = _context.Feriado.FirstOrDefault(f => f.CodFeriado == codFeriado);

            if (f != null)
            {
                _context.Feriado.Remove(f);
                _context.SaveChanges();
            }
        }

        public Feriado ObterPorCodigo(int codigo)
        {
            return _context.Feriado.FirstOrDefault(f => f.CodFeriado == codigo);
        }

        public PagedList<Feriado> ObterPorParametros(FeriadoParameters parameters)
        {
            var feriados = _context.Feriado
                .Include(f => f.Cidade)
                .Include(f => f.UnidadeFederativa)
                .Include(f => f.Pais)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                feriados = feriados.Where(
                            f =>
                            f.CodFeriado.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            f.NomeFeriado.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }


            if (parameters.CodFeriado != null)
            {
                feriados = feriados.Where(f => f.CodFeriado == parameters.CodFeriado);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodCidades))
            {
                int[] cods = parameters.CodCidades.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                feriados = feriados.Where(f => cods.Contains(f.CodCidade.Value));
            }
            if (!string.IsNullOrWhiteSpace(parameters.CodUfs))
            {
                int[] cods = parameters.CodUfs.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                feriados = feriados.Where(f => cods.Contains(f.CodUF.Value));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                feriados = feriados.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Feriado>.ToPagedList(feriados, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// Calcula os dias não uteis em um período
        /// </summary>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <param name="contabilizarSabado"></param>
        /// <param name="contabilizarDomingo"></param>
        /// <param name="contabilizarFeriados"></param>
        /// <param name="codCidade"></param>
        /// <returns></returns>
        public int CalculaDiasNaoUteis(DateTime dataInicio, DateTime dataFim, bool contabilizarSabado = false, bool contabilizarDomingo = false, bool contabilizarFeriados = false, int? codCidade = null)
        {
            List<DateTime> feriadosNoAno = new();
            int ano = DateTime.Now.Year;

            feriadosNoAno.Add(new DateTime(ano, 1, 1)); //Ano novo 
            feriadosNoAno.Add(new DateTime(ano, 4, 21));  //Tiradentes
            feriadosNoAno.Add(new DateTime(ano, 5, 1)); //Dia do trabalho
            feriadosNoAno.Add(new DateTime(ano, 9, 7)); //Dia da Independência do Brasil
            feriadosNoAno.Add(new DateTime(ano, 10, 12));  //Nossa Senhora Aparecida
            feriadosNoAno.Add(new DateTime(ano, 11, 2)); //Finados
            feriadosNoAno.Add(new DateTime(ano, 11, 15)); //Proclamação da República
            feriadosNoAno.Add(new DateTime(ano, 12, 25)); //Natal

            #region Feriados Móveis - Anuais

            int x, y;
            int a, b, c, d, e;
            int day, month;

            if (ano >= 1900 & ano <= 2099)
            {
                x = 24;
                y = 5;
            }
            else
                if (ano >= 2100 & ano <= 2199)
            {
                x = 24;
                y = 6;
            }
            else
                    if (ano >= 2200 & ano <= 2299)
            {
                x = 25;
                y = 7;
            }
            else
            {
                x = 24;
                y = 5;
            }

            a = ano % 19;
            b = ano % 4;
            c = ano % 7;
            d = (19 * a + x) % 30;
            e = (2 * b + 4 * c + 6 * d + y) % 7;

            if ((d + e) > 9)
            {
                day = (d + e - 9);
                month = 4;
            }

            else
            {
                day = (d + e + 22);
                month = 3;
            }

            var pascoa = new DateTime(ano, month, day);
            var sextaSanta = pascoa.AddDays(-2);
            var carnaval = pascoa.AddDays(-47);
            var corpusChristi = pascoa.AddDays(60);

            feriadosNoAno.Add(pascoa);
            feriadosNoAno.Add(sextaSanta);
            feriadosNoAno.Add(carnaval);
            feriadosNoAno.Add(corpusChristi);

            #endregion

            Feriado[] feriadosSAT = this.listaFeriados.Where(d => d.Data >= dataInicio && d.Data <= dataFim).ToArray();// _context.Feriado.Where(d => d.Data >= dataInicio && d.Data <= dataFim).ToArray();

            int diasNaoUteis = 0;

            while (dataInicio <= dataFim)
            {
                if (dataInicio.DayOfWeek == DayOfWeek.Saturday)
                {
                    diasNaoUteis += contabilizarSabado ? 0 : 1;
                }
                else if (dataInicio.DayOfWeek == DayOfWeek.Sunday)
                {
                    diasNaoUteis += contabilizarDomingo ? 0 : 1;
                }
                else if (feriadosSAT.Any(c => codCidade != null && c.CodCidade == codCidade.Value && c.Data.Value.Date == dataInicio.Date) ||
                    feriadosNoAno.Any(data => data.Date == dataInicio.Date))
                {
                    diasNaoUteis += contabilizarFeriados ? 0 : 1;
                }

                dataInicio = dataInicio.AddDays(1);
            }

            return diasNaoUteis;
        }
    }
}
