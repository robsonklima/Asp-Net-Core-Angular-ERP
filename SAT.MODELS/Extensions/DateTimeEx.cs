using System;
using System.Globalization;

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

        public static DateTime SetDate(DateTime date2) => new DateTime(date2.Year, date2.Month, date2.Day);
        
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

        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }
    }
}
