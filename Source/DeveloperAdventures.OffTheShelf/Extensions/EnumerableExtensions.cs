namespace DeveloperAdventures.OffTheShelf.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static bool IsNull<T>(this IEnumerable<T> items)
        {
            return items == null;
        }

        public static string Flatten(this IList<string> items)
        {
            var flattened = items.Aggregate(string.Empty, (current, item) => current + string.Format("{0},", item));

            if (flattened.EndsWith(","))
            {
                flattened = flattened.Substring(0, flattened.Length - 1);
            }

            return flattened;
        }

        public static string Flatten(this IList<int> items)
        {
            var flattened = items.Aggregate(string.Empty, (current, item) => current + string.Format("{0},", item));

            if (flattened.EndsWith(","))
            {
                flattened = flattened.Substring(0, flattened.Length - 1);
            }

            return flattened;
        }
    }
}