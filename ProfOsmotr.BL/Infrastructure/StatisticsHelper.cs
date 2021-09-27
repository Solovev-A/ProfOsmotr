using System;
using System.Collections.Generic;

namespace ProfOsmotr.BL.Infrastructure
{
    public static class StatisticsHelper
    {
        public static IEnumerable<string> GetPeriodsRange(string startPeriod)
        {
            if (startPeriod is null) throw new ArgumentNullException(nameof(startPeriod));

            var period = startPeriod.Split('-');
            if (period.Length != 2) throw new ArgumentException("Неверный формат периода");

            if (!int.TryParse(period[0], out int month) || month < 0 || month > 12)
                throw new ArgumentException("Неверный формат месяца");

            if (!int.TryParse(period[1], out int year) || year < 0 || year > 9999)
                throw new ArgumentException("Неверный формат года");

            var result = new List<string>();
            var currentDate = new DateTime(year, month, 1);

            while (true)
            {
                var newPeriod = $"{currentDate.Month:D2}-{currentDate.Year}";
                result.Add(newPeriod);

                var now = DateTime.Now;
                if (currentDate.Month == now.Month && currentDate.Year == now.Year)
                    break;

                currentDate = currentDate.AddMonths(1);
            }

            return result;
        }
    }
}