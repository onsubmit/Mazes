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
    }
}
