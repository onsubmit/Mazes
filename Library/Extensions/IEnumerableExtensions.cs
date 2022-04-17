//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Gets a random element from the list.
        /// </summary>
        /// <typeparam name="T">The list type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="element">The element assuming guard clauses are satisfied.</param>
        /// <returns><c>true</c> if a random element was successfully found, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the list is <c>null</c>.</exception>
        public static bool TryGetRandomElement<T>(this IEnumerable<T> list, out T element)
        {
            element = default!;
            if (list == null)
            {
                return false;
            }

            if (!list.Any())
            {
                return false;
            }

            int index = Rand.Instance.Next(0, list.Count());
            element = list.ElementAt(index);
            return true;
        }

        /// <summary>
        /// Gets a random element from the list.
        /// </summary>
        /// <typeparam name="T">The list type.</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>The random element.</returns>
        /// <exception cref="ArgumentNullException">Throw when list is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if list is empty.</exception>
        public static T GetRandomElement<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (!list.Any())
            {
                throw new InvalidOperationException("List is empty.");
            }

            int index = Rand.Instance.Next(0, list.Count());
            return list.ElementAt(index);
        }
    }
}
