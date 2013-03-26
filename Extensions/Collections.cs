namespace System.Collections.Generic
{
    /// <summary>
    /// Provides useful extensions to various generic collection classes
    /// </summary>
    public static class TBXCollections
    {
        /// <summary>
        /// Safer getter for dictionary that returns null on non-existant key for
        /// reference types
        /// </summary>
        public static TV GetOrNullR<T, TV>(this Dictionary<T, TV> dict, T key)
            where TV : class
        {
            if (dict.ContainsKey(key))
                return dict[key];
            else
                return null;
        }

        /// <summary>
        /// Safer getter for dictionary that returns default on non-existant key for value
        /// types
        /// </summary>
        public static TV GetOrNullV<T, TV>(this Dictionary<T, TV> dict, T key)
            where TV : struct
        {
            if (dict.ContainsKey(key))
                return dict[key];
            else
                return default(TV);
        }

        /// <summary>
        /// Implementation of AddRange for ISets
        /// </summary>
        public static void AddRange<T>(this ISet<T> list, IEnumerable<T> elements)
        {
            foreach (var item in elements) list.Add(item);
        }
    }
}
