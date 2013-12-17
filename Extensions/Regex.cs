using System.Linq;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Provides useful extensions to the Regex class
    /// </summary>
    public static class TRegex
    {
        /// <summary>
        /// Converts all groups of a match to simple string array
        /// </summary>
        public static string[] ToArray(this Match match)
        {
            var query = from   Group grp in match.Groups
                        select grp.Value;

            return query.ToArray();
        }

        /// <summary>
        /// Shortcut to <see cref="Regex.Match(string, string)"/> with auto
        /// case-insensitivity
        /// </summary>
        public static Match Match(string input, string pattern)
        {
            return Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Shortcut to <see cref="Regex.Replace(string, string, string)"/> with auto
        /// case-insensitivity
        /// </summary>
        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Shortcut to <see cref="Regex.Replace(string, string, MatchEvaluator)"/> with
        /// auto case-insensitivity
        /// </summary>
        public static string Replace(string input, string pattern, MatchEvaluator eval)
        {
            return Regex.Replace(input, pattern, eval, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Shortcut to <see cref="Regex.IsMatch(string, string)"/> with auto
        /// case-insensitivity
        /// </summary>
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Shortcut to using Regex.Match in if conditionals
        /// </summary>
        /// <param name="regex">Regex object to match</param>
        /// <param name="input">String to try find matches in</param>
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
        /// <param name="input">String to try find matches in</param>
        /// <param name="pattern">Regex pattern to match</param>
        /// <param name="matches">Outputs a list of matches, null if successful</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool TryMatch(string input, string pattern, out string[] matches)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase)
                .TryMatch(input, out matches);
        }
    }
}
