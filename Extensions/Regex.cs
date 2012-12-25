using System.Linq;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Provides useful extensions to the Regex class
    /// </summary>
    public static class TBXRegex
    {
        /// <summary>
        /// Converts all groups of a match to simple string array
        /// </summary>
        public static string[] ToArray(this Match match)
        {
            return (from Group grp in match.Groups select grp.Value).ToArray();
        }

        /// <summary>
        /// Shortcut to Regex.IsMatch() with auto case-insensitivity
        /// </summary>
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Shortcut to using Regex.Match in if conditionals
        /// </summary>
        /// <param name="matches">Outputs a list of matches, null if unsuccessful</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool TryMatch(this Regex regex, string input, out string[] matches)
        {
            var match = regex.Match(input);
            matches = match.Success
                ? (from Group m in match.Groups select m.Value).ToArray()
                : null;

            return match.Success;
        }

        /// <summary>
        /// Shortcut of Regex.TryMatch extension, which creates a regex that assumes
        /// IgnoreCase
        /// </summary>
        /// <param name="matches">Outputs a list of matches, null if successful</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool TryMatch(string input, string pattern, out string[] matches)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase)
                .TryMatch(input, out matches);
        }
    }
}
