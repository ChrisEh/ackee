using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Ackee.Shared.Services
{
    public class DateTimeService
    {
        // Check: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netframework-4.8
        public string GetDateAsString(string format, DateTime date)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            return date.ToString(format, culture);
        }

        // 0 is current week, 1 is next week, -1 is previous week, etc.
        // Assume week starts at monday
        public List<DateTime> GetAllDatesPerWeek(int weekNr)
        {
            // Determine the right week.
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime lastSunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = lastSunday.AddDays(1);

            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }

            DateTime mondayOfAskedWeek = monday.AddDays(0 + 7 * weekNr);
            var datesOfAskedWeek = Enumerable.Range(0, 7).Select(days => mondayOfAskedWeek.AddDays(days)).ToList();

            return datesOfAskedWeek;
        }
    }
}
