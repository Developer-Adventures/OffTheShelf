namespace DeveloperAdventures.OffTheSelf.Extensions
{
    using System;

    public static class DateTimeExtensions
	{
		public static int PeriodsBetween(this DateTime date1, DateTime date2)
		{
			if (date1.Month == date2.Month && date1.Year == date2.Year)
			{
				return 0;
			}

			return date1 > date2 ? Math.Abs(((date2.Year - date1.Year) * 12) + (date2.Month - date1.Month)) : Math.Abs(((date1.Year - date2.Year) * 12) + (date1.Month - date2.Month));
		}

		public static DateTime BeginningOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime EndOfMonth(this DateTime date)
		{
			return date.BeginningOfMonth().AddMonths(1).AddDays(-1);
		}

		public static string ToPaddedShortDateString(this DateTime date)
		{
			return date.ToString("MM/dd/yyyy");
		}

	    public static DateTime NoMilliSeconds(this DateTime date)
	    {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
	    }
	}
}