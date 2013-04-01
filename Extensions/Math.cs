namespace System
{
    /// <summary>
    /// Provides useful extensions to numerical types
    /// </summary>
    public static class TMath
    {
        const string errorMinMoreThanMax = "Minimum value cannot be more than the maximum";

        /// <summary>
        /// Clamps a given integer between a minimum and maximum
        /// </summary>
        public static int Clamp( this int value, int min, int max)
        {
            if (min > max)
                throw new ArgumentException(errorMinMoreThanMax);

            if      (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        /// <summary>
        /// Clamps a given short between a minimum and maximum
        /// </summary>
        public static short Clamp( this short value, short min, short max)
        {
            if (min > max)
                throw new ArgumentException(errorMinMoreThanMax);

            if      (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
    }
}
