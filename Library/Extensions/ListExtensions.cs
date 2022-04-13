//-----------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="List{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds the element to the list if it's not null.
        /// </summary>
        /// <typeparam name="T">The list and element type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="element">The element.</param>
        public static void AddIfNotNull<T>(this List<T> list, T? element)
        {
            if (list == null)
            {
                return;
            }

            if (element == null)
            {
                return;
            }

            list.Add(element);
        }

        /// <summary>
        /// Gets a random element from the list.
        /// </summary>
        /// <typeparam name="T">The list type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="element">The element assuming guard clauses are satisfied.</param>
        /// <returns><c>true</c> if a random element was successfully found, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the list is <c>null</c>.</exception>
        public static bool TryGetRandomElement<T>(this List<T> list, out T element)
        {
            element = default!;
            if (list == null)
            {
                return false;
            }

            if (list.Count == 0)
            {
                return false;
            }

            int index = Rand.Instance.Next(0, list.Count);
            element = list[index];
            return true;
        }
    }
}
