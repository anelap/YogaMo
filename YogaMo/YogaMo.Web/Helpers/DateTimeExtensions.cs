using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.Helpers
{

    public static class DateTimeExtensions
    {
        public static long MillisecondsTimestamp(this DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }
    }
}
