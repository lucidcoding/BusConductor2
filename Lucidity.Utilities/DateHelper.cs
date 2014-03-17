using System;

namespace Lucidity.Utilities
{
    public static class DateHelper
    {
        //Assumes first day of week is Monday
        public static DateTime GetFirstDayOfFirstPartWeekOfMonth(int year, int month)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            DateTime firstDayOfFirstPartWeekOfMonth;

            if (firstDayOfMonth.DayOfWeek != DayOfWeek.Sunday)
            {
                firstDayOfFirstPartWeekOfMonth =
                    firstDayOfMonth.AddDays(DayOfWeek.Monday - firstDayOfMonth.DayOfWeek);
            }
            else
            {
                firstDayOfFirstPartWeekOfMonth = firstDayOfMonth.AddDays(-6);
            }

            return firstDayOfFirstPartWeekOfMonth;
        }

        public static DateTime GetFirstDayOfFirstFullWeekOfMonth(int year, int month)
        {
            var firstDayOfFirstPartWeekOfMonth = GetFirstDayOfFirstPartWeekOfMonth(year, month);

            if(firstDayOfFirstPartWeekOfMonth.Day == 1)
            {
                return firstDayOfFirstPartWeekOfMonth;
            }

            return firstDayOfFirstPartWeekOfMonth.AddDays(7);
        }
    }
}
