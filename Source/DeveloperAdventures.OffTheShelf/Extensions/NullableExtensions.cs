namespace DeveloperAdventures.OffTheShelf.Extensions
{
    using System;

    public static class NullableExtensions
    {
        public static string NullableDateTimeToShortDateString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToShortDateString() : string.Empty;
        }

        public static decimal NullableDecimalToFriendlyValue(this decimal? nullable)
        {
            return nullable.HasValue ? nullable.Value : 0;
        }

        public static decimal ToDefaultValue(this decimal? nullable)
        {
            return nullable.HasValue ? nullable.Value : 0;
        }

        public static string ToDollarString(this decimal? nullable)
        {
            return nullable.HasValue ? nullable.Value.ToString("c") : "$0.00";
        }
    }
}