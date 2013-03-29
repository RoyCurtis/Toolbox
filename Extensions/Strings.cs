using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Provides useful extensions to strings
    /// </summary>
    public static class TBXString
    {
        /// <summary>
        /// Shortcut to string.Split(char[]) that removes empty entries by default
        /// </summary>
        public static string[] TerseSplit( this string str, params char[] separators )
        {
            return str.Split( separators, StringSplitOptions.RemoveEmptyEntries );
        }

        /// <summary>
        /// Shortcut to string.Split(string[]) that removes empty entries by default
        /// </summary>
        public static string[] TerseSplit( this string str, params string[] separators )
        {
            return str.Split( separators, StringSplitOptions.RemoveEmptyEntries );
        }

        /// <summary>
        /// Case-insensitive string equality shortcut (current culture)
        /// </summary>
        public static bool IEquals( this string source, string value )
        {
            return source.Equals( value, StringComparison.InvariantCultureIgnoreCase );
        }

        /// <summary>
        /// Shortcut to string.Format, so that string literals can easily be formatted
        /// </summary>
        public static string LFormat( this string source, params object[] parts )
        {
            return string.Format(source, parts);
        }
    }
}