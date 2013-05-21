namespace DeveloperAdventures.OffTheSelf.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        /// <summary>
        /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToEncrypt">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }

            var cspp = new CspParameters();
            cspp.KeyContainerName = key;

            var rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(stringToEncrypt), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToDecrypt">String that must be decrypted.</param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(stringToDecrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            try
            {
                var cspp = new CspParameters();
                cspp.KeyContainerName = key;

                var rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = Encoding.UTF8.GetString(bytes);
            }
            finally
            {
                // no need for further processing
            }

            return result;
        }

        /// <summary>
        /// Determines if passed in string is a valid URL
        /// </summary>
        /// <param name="text">String used to validate if is a valid URL</param>
        /// <returns>True or False</returns>
        public static bool IsValidUrl(this string text)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }

        /// <summary>
        /// Returns the last few characters of the string with a length
        /// specified by the given parameter. If the string's length is less than the 
        /// given length the complete string is returned. If length is zero or 
        /// less an empty string is returned
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="length">Number of characters to return</param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            length = Math.Max(length, 0);
            return s.Length > length ? s.Substring(s.Length - length, length) : s;
        }

        /// <summary>
        /// Returns the first few characters of the string with a length
        /// specified by the given parameter. If the string's length is less than the 
        /// given length the complete string is returned. If length is zero or 
        /// less an empty string is returned
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="length">Number of characters to return</param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            length = Math.Max(length, 0);
            return s.Length > length ? s.Substring(0, length) : s;
        }

        /// <summary>
        /// Strip a string of the specified character.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="char">character to remove from the string</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip('b');  //s becomes 'acde;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, char character)
        {
            s = s.Replace(character.ToString(), string.Empty);

            return s;
        }

        /// <summary>
        /// Strip a string of the specified characters.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="chars">list of characters to remove from the string</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip('a', 'd');  //s becomes 'bce;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, params char[] chars)
        {
            return chars.Aggregate(s, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        /// <summary>
        /// Strip a string of the specified substring.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="subString">substring to remove</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip("bcd");  //s becomes 'ae;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, string subString)
        {
            s = s.Replace(subString, string.Empty);

            return s;
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            var truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            var strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        /// <summary>
        /// Checks the passed in string to determine if it is NOT null or empty
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>Bool.</returns>
        public static bool IsNotNullOrEmpty(this string input)
        {
            return !String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Checks to see if passed in string is null or empty
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>Bool.</returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines if the passed in string contains any of the chars in the char[]
        /// </summary>
        /// <param name="theString">String to check against character array.</param>
        /// <param name="characters">Array of char to search theString for.</param>
        /// <returns>Bool.</returns>
        public static bool ContainsAny(this string theString, char[] characters)
        {
            return characters.Any(character => theString.Contains(character.ToString()));
        }

        public static string Reverse(this string s)
        {
            char[] c = s.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public static T Parse<T>(this string value)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            T result = default(T);
            if (!string.IsNullOrEmpty(value))
            {
                // we are not going to handle exception here
                // if you need SafeParse then you should create
                // another method specially for that.
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            }
            return result;
        }

        public static bool IsNumeric(this string value)
        {
            var regex = new Regex(@"^[0-9]$");
            return regex.IsMatch(value);
        }

        public static bool ToBoolean(this string value)
        {
            if (value == bool.FalseString.ToLower())
                return false;

            if (value == string.Empty)
                return false;

            return value.IndexOf(bool.TrueString.ToLower()) > -1;
        }

        public static DateTime ToDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }

        public static int ToInt32(this string value)
        {
            var returnNumber = 0;

            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            return int.TryParse(value, out returnNumber) ? returnNumber : 0;
        }

        public static double ToDouble(this string value)
        {
            double returnNumber = 0;

            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            return double.TryParse(value, out returnNumber) ? returnNumber : 0;
        }

        /// <summary>
        /// Base64 encodes a string.
        /// </summary>
        /// <param name="input">A string</param>
        /// <returns>A base64 encoded string</returns>
        public static string Base64StringEncode(this string input)
        {
            var encbuff = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64 decodes a string.
        /// </summary>
        /// <param name="input">A base64 encoded string</param>
        /// <returns>A decoded string</returns>
        public static string Base64StringDecode(this string input)
        {
            var decbuff = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// A case insenstive replace function.
        /// </summary>
        /// <param name="input">The string to examine.</param>
        /// <param name="newValue">The value to replace.</param>
        /// <param name="oldValue">The new value to be inserted</param>
        /// <returns>A string</returns>
        public static string CaseInsenstiveReplace(this string input, string newValue, string oldValue)
        {
            var regEx = new Regex(oldValue, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(input, newValue);
        }

        /// <summary>
        /// Removes all the words passed in the filter words parameters. The replace is NOT case
        /// sensitive.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <param name="filterWords">The words to repace in the input string.</param>
        /// <returns>A string.</returns>
        public static string FilterWords(this string input, params string[] filterWords)
        {
            return FilterWords(input, char.MinValue, filterWords);
        }

        /// <summary>
        /// Removes all the words passed in the filter words parameters. The replace is NOT case
        /// sensitive.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <param name="mask">A character that is inserted for each letter of the replaced word.</param>
        /// <param name="filterWords">The words to repace in the input string.</param>
        /// <returns>A string.</returns>
        public static string FilterWords(this string input, char mask, params string[] filterWords)
        {
            var stringMask = mask == char.MinValue ? string.Empty : mask.ToString();
            var totalMask = stringMask;

            foreach (var s in filterWords)
            {
                var regEx = new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                if (stringMask.Length > 0)
                {
                    for (var i = 1; i < s.Length; i++)
                        totalMask += stringMask;
                }

                input = regEx.Replace(input, totalMask);

                totalMask = stringMask;
            }

            return input;
        }

        /// <summary>
        /// MD5 encodes the passed string
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string MD5String(this string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Verified a string against the passed MD5 hash.
        /// </summary>
        /// <param name="input">The string to compare.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>True if the input and the hash are the same, false otherwise.</returns>
        public static bool MD5VerifyString(this string input, string hash)
        {
            // Hash the input.
            var hashOfInput = MD5String(input);

            // Create a StringComparer an comare the hashes.
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

        /// <summary>
        /// Left pads the passed input using the HTML non-breaking string entity (&nbsp;)
        /// for the total number of spaces.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="totalSpaces">The total number to pad the string.</param>
        /// <returns>A padded string.</returns>
        public static string PadLeftHtmlSpaces(this string input, int totalSpaces)
        {
            var space = "&nbsp;";
            return PadLeft(input, space, totalSpaces * space.Length);
        }

        /// <summary>
        /// Left pads the passed input using the passed pad string
        /// for the total number of spaces.  It will not cut-off the pad even if it 
        /// causes the string to exceed the total width.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="pad">The string to uses as padding.</param>
        /// <param name="totalWidth">The total number to pad the string.</param>
        /// <returns>A padded string.</returns>
        public static string PadLeft(this string input, string pad, int totalWidth)
        {
            return PadLeft(input, pad, totalWidth, false);
        }

        /// <summary>
        /// Left pads the passed input using the passed pad string
        /// for the total number of spaces.  It will cut-off the pad so that  
        /// the string does not exceed the total width.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="pad">The string to uses as padding.</param>
        /// <param name="totalWidth">The total number to pad the string.</param>
        /// <param name="cutOff"></param>
        /// <returns>A padded string.</returns>
        public static string PadLeft(this string input, string pad, int totalWidth, bool cutOff)
        {
            if (input.Length >= totalWidth)
                return input;

            var paddedString = input;

            while (paddedString.Length < totalWidth)
            {
                paddedString += pad;
            }

            // trim the excess.
            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth);

            return paddedString;
        }

        /// <summary>
        /// Right pads the passed input using the HTML non-breaking string entity (&nbsp;)
        /// for the total number of spaces.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="totalSpaces">The total number to pad the string.</param>
        /// <returns>A padded string.</returns>
        public static string PadRightHtmlSpaces(this string input, int totalSpaces)
        {
            var space = "&nbsp;";
            return PadRight(input, space, totalSpaces * space.Length);
        }

        /// <summary>
        /// Right pads the passed input using the passed pad string
        /// for the total number of spaces.  It will not cut-off the pad even if it 
        /// causes the string to exceed the total width.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="pad">The string to uses as padding.</param>
        /// <param name="totalWidth">The total number to pad the string.</param>
        /// <returns>A padded string.</returns>
        public static string PadRight(this string input, string pad, int totalWidth)
        {
            return PadRight(input, pad, totalWidth, false);
        }

        /// <summary>
        /// Right pads the passed input using the passed pad string
        /// for the total number of spaces.  It will cut-off the pad so that  
        /// the string does not exceed the total width.
        /// </summary>
        /// <param name="input">The string to pad.</param>
        /// <param name="pad">The string to uses as padding.</param>
        /// <param name="totalWidth">The total number to pad the string.</param>
        /// <param name="cutOff"></param>
        /// <returns>A padded string.</returns>
        public static string PadRight(this string input, string pad, int totalWidth, bool cutOff)
        {
            if (input.Length >= totalWidth)
                return input;

            var paddedString = string.Empty;

            while (paddedString.Length < totalWidth - input.Length)
            {
                paddedString += pad;
            }

            // trim the excess.
            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth - input.Length);

            paddedString += input;

            return paddedString;
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <returns>A string</returns>
        public static string RemoveNewLines(this string input)
        {
            return RemoveNewLines(input, false);
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.
        /// </summary>
        /// <param name="input">The string to search.</param>
        /// <param name="addSpace">If true, adds a space (" ") for each newline and carriage
        /// return found.</param>
        /// <returns>A string</returns>
        public static string RemoveNewLines(this string input, bool addSpace)
        {
            var replace = string.Empty;
            if (addSpace)
                replace = " ";

            var pattern = @"[\r|\n]";
            var regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return regEx.Replace(input, replace);
        }

        /// <summary>
        /// Converts a string to sentence case.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string</returns>
        public static string SentenceCase(this string input)
        {
            if (input.Length < 1)
                return input;

            var sentence = input.ToLower();
            return sentence[0].ToString().ToUpper() + sentence.Substring(1);
        }

        /// <summary>
        /// Converts all spaces to HTML non-breaking spaces
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string</returns>
        public static string SpaceToNbsp(this string input)
        {
            var space = "&nbsp;";
            return input.Replace(" ", space);
        }

        /// <summary>
        /// Removes all HTML tags from the passed string
        /// </summary>
        /// <param name="input">The string whose values should be replaced.</param>
        /// <returns>A string.</returns>
        public static string StripTags(this string input)
        {
            var stripTags = new Regex("<(.|\n)+?>");
            return stripTags.Replace(input, string.Empty);
        }

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string.</returns>
        public static string TitleCase(this string input)
        {
            return TitleCase(input, true);
        }

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="ignoreShortWords">If true, does not capitalize words like
        /// "a", "is", "the", etc.</param>
        /// <returns>A string.</returns>
        public static string TitleCase(this string input, bool ignoreShortWords)
        {
            List<string> ignoreWords = null;
            if (ignoreShortWords)
            {
                ignoreWords = new List<string> { "a", "is", "was", "the" };
            }

            var tokens = input.Split(' ');
            var sb = new StringBuilder(input.Length);
            foreach (var s in tokens)
            {
                if (s != string.Empty)
                {
                    if (ignoreShortWords && s != tokens[0] && ignoreWords.Contains(s.ToLower()))
                    {
                        sb.Append(s + " ");
                    }
                    else if (s.EndsWith(".") || s.Contains("."))
                    {
                        sb.Append(s);
                    }
                    else
                    {
                        sb.Append(s[0].ToString().ToUpper());
                        sb.Append(s.Substring(1).ToLower());
                        sb.Append(" ");
                    }
                }
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Removes multiple spaces between words
        /// </summary>
        /// <param name="input">The string to trim.</param>
        /// <returns>A string.</returns>
        public static string TrimIntraWords(this string input)
        {
            var regEx = new Regex(@"[\s]+");
            return regEx.Replace(input, " ");
        }

        /// <summary>
        /// Converts new line(\n) and carriage return(\r) symbols to
        /// HTML line breaks.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string.</returns>
        public static string NewLineToBreak(this string input)
        {
            var regEx = new Regex(@"[\n|\r]+");
            return regEx.Replace(input, "<br />");
        }

        /// <summary>
        /// Wraps the passed string up the 
        /// until the next whitespace on or after the total charCount has been reached
        /// for that line.  Uses the environment new line
        /// symbol for the break text.
        /// </summary>
        /// <param name="input">The string to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <returns>A string.</returns>
        public static string WordWrap(this string input, int charCount)
        {
            return WordWrap(input, charCount, false, Environment.NewLine);
        }

        /// <summary>
        /// Wraps the passed string up the total number of characters (if cuttOff is true)
        /// or until the next whitespace (if cutOff is false).  Uses the environment new line
        /// symbol for the break text.
        /// </summary>
        /// <param name="input">The string to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <param name="cutOff">If true, will break in the middle of a word.</param>
        /// <returns>A string.</returns>
        public static string WordWrap(this string input, int charCount, bool cutOff)
        {
            return WordWrap(input, charCount, cutOff, Environment.NewLine);
        }

        /// <summary>
        /// Wraps the passed string up the total number of characters (if cuttOff is true)
        /// or until the next whitespace (if cutOff is false).  Uses the passed breakText
        /// for lineBreaks.
        /// </summary>
        /// <param name="input">The string to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <param name="cutOff">If true, will break in the middle of a word.</param>
        /// <param name="breakText">The line break text to use.</param>
        /// <returns>A string.</returns>
        public static string WordWrap(this string input, int charCount, bool cutOff, string breakText)
        {
            var sb = new StringBuilder(input.Length + 100);
            var counter = 0;

            if (cutOff)
            {
                while (counter < input.Length)
                {
                    if (input.Length > counter + charCount)
                    {
                        sb.Append(input.Substring(counter, charCount));
                        sb.Append(breakText);
                    }
                    else
                    {
                        sb.Append(input.Substring(counter));
                    }
                    counter += charCount;
                }
            }
            else
            {
                var strings = input.Split(' ');
                for (var i = 0; i < strings.Length; i++)
                {
                    counter += strings[i].Length + 1; // the added one is to represent the inclusion of the space.
                    if (i != 0 && counter > charCount)
                    {
                        sb.Append(breakText);
                        counter = 0;
                    }

                    sb.Append(strings[i] + ' ');
                }
            }
            return sb.ToString().TrimEnd(); // to get rid of the extra space at the end.
        }

        /// <summary>
        /// Takes an enum name as a string and replaces "_" with " " for outputting to a view
        /// </summary>
        /// <param name="dirtyName">name of enum e.g. enumname.ToString</param>
        /// <returns></returns>
        public static string CleanNameFromEnum(this string dirtyName)
        {
            return dirtyName.Replace("_", " ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirtyName"></param>
        /// <param name="charToReplaceWith"></param>
        /// <returns></returns>
        public static string CleanNameFromEnum(this string dirtyName, string charToReplaceWith)
        {
            return dirtyName.Replace("_", charToReplaceWith);
        }

        /// <summary>
        /// Formats phone number from 5551112222 to (555) 111-2222
        /// </summary>
        /// <param name="s">unformatted phone number</param>
        /// <returns></returns>
        public static string FormatPhoneNumber(this string s)
        {
            if (!string.IsNullOrEmpty(s) && s.Length > 9)
                return "(" + s.Substring(0, 3) + ")" + " " + s.Substring(3, 3) + "-" + s.Substring(6, 4);

            return s;
        }

        public static string FormatSSN(this string s)
        {
            if (s != string.Empty)
            {
                s = s.Replace("-", string.Empty);

                if (s.Length == 9)
                {
                    return string.Format("{0:###-##-####}", s.ToInt32());
                }

                return s;
            }

            return s;
        }
    }
}