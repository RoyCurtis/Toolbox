using System.Text.RegularExpressions;
using System.Linq;

namespace System
{
    /// <summary>
    /// Provides useful extensions to strings
    /// </summary>
    public static class TString
    {
        /// <summary>
        /// Shortcut to string.Split(char[]) that trims all entries and removes any that
        /// are whitespace or empty
        /// </summary>
        public static string[] TerseSplit(this string str, params char[] separators)
        {
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select( entry => entry.Trim() )
                .Where( entry => !string.IsNullOrWhiteSpace(entry) )
                .ToArray();
        }

        /// <summary>
        /// Shortcut to string.Split(string[]) that trims all entries and removes any that
        /// are whitespace or empty
        /// </summary>
        public static string[] TerseSplit(this string str, params string[] separators)
        {
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select( entry => entry.Trim() )
                .Where( entry => !string.IsNullOrWhiteSpace(entry) )
                .ToArray();
        }

        /// <summary>
        /// Case-insensitive string equality shortcut (ordinal)
        /// </summary>
        public static bool IEquals(this string source, string value)
        {
            return source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Shortcut to string.Format, so that string literals can easily be formatted
        /// </summary>
        public static string LFormat(this string source, params object[] parts)
        {
            return string.Format(source, parts);
        }

        /// <summary>
        /// Does regex replacements that map string pairs; first in the pair is the
        /// regex pattern to match, second is the text to replace with
        /// </summary>
        public static string Map(this string str, bool ignoreCase, params string[] mapping)
        {
            if (mapping.Length % 2 != 0)
                throw new ArgumentException("Odd number of arguments provided for mapping; even expected");

            for (var i = 0; i < mapping.Length; i += 2)
            {
                var target  = mapping[i];
                var replace = mapping[i + 1];
                var opt     = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

                str = Regex.Replace(str, target, replace, opt);
            }

            return str;
        }

        /// <summary>
        /// Does case-sensitive regex replacements that map string pairs; first in the
        /// pair is the regex pattern to match, second is the text to replace with.
        /// </summary>
        public static string Map(this string str, params string[] mapping)
        {
            return str.Map(false, mapping);
        }
    }
}