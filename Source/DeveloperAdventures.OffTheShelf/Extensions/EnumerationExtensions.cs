namespace DeveloperAdventures.OffTheShelf.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumExtensions
    {
        /// <summary>
        /// Converts a sting into an element of the specified enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumString"></param>
        /// <returns></returns>
        public static T ConvertStringToEnum<T>(string enumString)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumString, true);
            }
            catch (Exception ex)
            {
                string s = string.Format("'{0}' is not a valid enumeration of '{1}'", enumString, typeof(T).Name);
                throw new Exception(s, ex);
            }
        }

        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            return new List<T>(Enum.GetValues(enumType) as IEnumerable<T>);
        }
    }
}