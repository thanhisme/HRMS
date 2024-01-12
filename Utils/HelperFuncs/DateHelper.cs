namespace Utils.HelperFuncs
{
    public class DateHelper
    {
        private const string TIMEZONE = "SE Asia Standard Time";
        public static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime GetCurrentDateTime(string timezone = TIMEZONE)
        {
            var desiredTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

            // Get the current time in the specified time zone
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, desiredTimeZone);
        }
    }
}
