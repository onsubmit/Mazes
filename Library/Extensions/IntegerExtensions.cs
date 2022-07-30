//-----------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="int"/>.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Performs the modulo operation.
        /// </summary>
        /// <param name="a">First integer.</param>
        /// <param name="b">Second integer.</param>
        /// <returns>The modulo.</returns>
        public static int Modulo(this int a, int b)
        {
            // https://stackoverflow.com/a/1082938
            return ((a % b) + b) % b;
        }

        /// <summary>
        /// Gets whether the integer is is even.
        /// </summary>
        /// <param name="n">The integer.</param>
        /// <returns><c>true</c> if the integer is even, <c>false</c> otherwise.</returns>
        public static bool IsEven(this int n)
        {
            return n % 2 == 0;
        }

        /// <summary>
        /// Gets whether the integer is is odd.
        /// </summary>
        /// <param name="n">The integer.</param>
        /// <returns><c>true</c> if the integer is odd, <c>false</c> otherwise.</returns>
        public static bool IsOdd(this int n) => !n.IsEven();
    }
}
