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
    }
}
