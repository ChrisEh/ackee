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
    }
}
