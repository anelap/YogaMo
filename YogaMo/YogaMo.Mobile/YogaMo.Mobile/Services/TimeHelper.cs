using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Mobile.Services
{
    public static class TimeHelper
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string GetMessageDateString(double timetoken)
        {
            string DatumText = null;
            DateTime Datum = TimeHelper.UnixTimeStampToDateTime(timetoken / 10000000);

            int DateDiff = (DateTime.Today - Datum.Date).Days;

            if (DateDiff == 0)
            {
                DatumText = Datum.ToShortTimeString();
            }
            else if (DateDiff == 1)
            {
                DatumText = "Yesterday " + Datum.ToShortTimeString();
            }
            else if (DateDiff > 1 && DateDiff < 7)
            {
                DatumText = Datum.DayOfWeek + " " + Datum.ToShortTimeString();
            }
            else if (DateDiff >= 7)
            {
                DatumText = Datum.Day + "." + Datum.Month + ". " + Datum.ToShortTimeString();
            }

            return DatumText;
        }
    }
}
