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
        public static bool IEquals ( this string source, string value )
        {
            return source.Equals( value, StringComparison.InvariantCultureIgnoreCase );
        }
    }

    /// <summary>
    /// Helper array for cleaner-looking operation of string.Split()
    /// </summary>
    public static class Split
    {
        /// <summary>
        /// Helper array for string.Split for splitting by commas
        /// </summary>
        public static char[] Comma   = new[] { ',' };
        /// <summary>
        /// Helper array for string.Split for splitting by dots
        /// </summary>
        public static char[] Dot     = new[] { '.' };
        /// <summary>
        /// Helper array for string.Split for splitting by forward slashes
        /// </summary>
        public static char[] FSlash  = new[] { '/' };
        /// <summary>
        /// Helper array for string.Split for splitting by backward slashes
        /// </summary>
        public static char[] BSlash  = new[] { '\\' };
        /// <summary>
        /// Helper array for string.Split for splitting by both slashes
        /// </summary>
        public static char[] Slashes = new[] { '/', '\\' };
    }
}