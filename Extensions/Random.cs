using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Provides a global static instance of <see cref="System.Random"/> to use
    /// </summary>
    public static class TRandom
    {
        /// <summary>
        /// Global <see cref="Random"/> object used by <see cref="TRandom"/>
        /// </summary>
        public static Random Random = new Random();

        /// <summary>
        /// Provides a random between 0 (inclusive) and a maximum value (exclusive)
        /// </summary>
        /// <param name="maxValue">Default is <see cref="int.MaxValue"/></param>
        public static int Next(int maxValue = int.MaxValue)
        {
            return Random.Next(maxValue);
        }

        /// <summary>
        /// Provides a random between a minimum value (inclusive) and a maximum value
        /// (exclusive)
        /// </summary>
        /// <param name="minValue">Inclusive minimum value</param>
        /// <param name="maxValue">
        /// Exclusive maximum value, must be greater than minValue
        /// </param>
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Provides a random float between 0 (inclusive) and a maximum value
        /// (exclusive)
        /// </summary>
        /// <param name="maxValue">Default is 1f</param>
        /// <returns></returns>
        public static float NextFloat(float maxValue = 1f)
        {
            return (float) Random.NextDouble() * maxValue;
        }

        /// <summary>
        /// Fills the provided buffer with random numbers
        /// </summary>
        /// <param name="buffer">Byte array to fill</param>
        public static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

        /// <summary>
        /// Provides a random boolean value
        /// </summary>
        /// <param name="favor">Percentage bias to favor toward a true value</param>
        public static bool NextBool(float favor = 50)
        {
            if (favor >= 100)
                return true;
            else
                return Random.Next(0, 100) < favor;
        }

        /// <summary>
        /// Chooses a random string from a list
        /// </summary>
        public static string NextElement(IEnumerable<string> list)
        {
            var index = Random.Next( 0, list.Count() );
            return list.ElementAt(index);
        }

        /// <summary>
        /// Chooses a random character from a list
        /// </summary>
        public static char NextElement(IEnumerable<char> list)
        {
            var index = Random.Next( 0, list.Count() );
            return list.ElementAt(index);
        }

        /// <summary>
        /// Chooses a random char from a string
        /// </summary>
        public static char NextLetter(string line)
        {
            var index = Random.Next(0, line.Length);
            return line[index];
        }
    }
}