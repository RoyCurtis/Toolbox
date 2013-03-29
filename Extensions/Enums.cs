﻿namespace System
{
    /// <summary>
    /// Provides useful extensions to enums
    /// </summary>
    public static class TBXEnums
    {
        /// <summary>
        /// Cleaner syntax for Enum.Parse(type, string)
        /// </summary>
        public static T Parse<T>( string value )
        {
            return (T) Enum.Parse(typeof(T), value);
        }
    }
}