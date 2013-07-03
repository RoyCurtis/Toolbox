namespace System
{
    /// <summary>
    /// Provides useful extensions to enums
    /// </summary>
    public static class TEnums
    {
        /// <summary>
        /// Cleaner syntax for Enum.Parse(type, string)
        /// </summary>
        public static T Parse<T>(string value, bool ignoreCase = true)
            where T : struct
        {
            return (T) Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}