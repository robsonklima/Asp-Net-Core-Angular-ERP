using System;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Extensions
{
    public static class DateTimeEx
    {
        public static DateTime GetFirstDayOfMonth(this DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(this DateTime now)
        {
            var from = now.GetFirstDayOfMonth();
            return from.AddMonths(1).AddDays(-1);
        }

        public static int GetMonthWorkingDays(this DateTime now, bool indSaturday = false, bool indSunday = false)
        {
            var from = now.GetFirstDayOfMonth();
            var to = now.GetLastDayOfMonth();

            var dayDifference = (to - from).Days + 1;

            return Enumerable
                .Range(1, dayDifference)
                .Select(d => from.AddDays(d))
                .Count(d => (indSaturday ? true : d.DayOfWeek != DayOfWeek.Saturday) &&
                            (indSunday ? true : d.DayOfWeek != DayOfWeek.Sunday));
        }

        public static int GetWorkingDaysBetweenTwoDates(DateTime from, DateTime to, bool indSaturday = false, bool indSunday = false)
        {
            var dayDifference = (to - from).Days + 1;

            return Enumerable
                .Range(1, dayDifference)
                .Select(d => from.AddDays(d))
                .Count(d => (indSaturday ? true : d.DayOfWeek != DayOfWeek.Saturday) &&
                            (indSunday ? true : d.DayOfWeek != DayOfWeek.Sunday));
        }

        public static DateTime? CalcHolidays(int year, FeriadoEnum holiday)
        {
            DateTime data = CalcEaster(year);

            switch (holiday)
            {
                case FeriadoEnum.Pascoa:
                    return null;
                case FeriadoEnum.Carnaval:
                    return data.AddDays(-47);
                case FeriadoEnum.QuartaCinzas:
                    return data.AddDays(-46);
                case FeriadoEnum.SextaSanta:
                    return data.AddDays(-2);
                case FeriadoEnum.CorpusChristi:
                    return data.AddDays(60);
            }
            return null;
        }

        public static DateTime SetDate(DateTime date2) => new DateTime(date2.Year, date2.Month, date2.Day);
        public static DateTime SetDate(DateTime? date2) => SetDate(date2.Value);

        private static DateTime CalcEaster(int year)
        {
            int r1 = year % 19;
            int r2 = year % 4;
            int r3 = year % 7;
            int r4 = (19 * r1 + 24) % 30;
            int r5 = (6 * r4 + 4 * r3 + 2 * r2 + 5) % 7;
            DateTime dataPascoa = new DateTime(year, 3, 22).AddDays(r4 + r5);
            int dia = dataPascoa.Day;
            switch (dia)
            {
                case 26:
                    dataPascoa = new DateTime(year, 4, 19);
                    break;
                case 25:
                    if (r1 > 10)
                        dataPascoa = new DateTime(year, 4, 18);
                    break;
            }
            return dataPascoa.Date;
        }
    }
}
