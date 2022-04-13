//-----------------------------------------------------------------------
// <copyright file="Rand.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using System;

    /// <summary>
    /// Holds a randomly seeded, singleton instance of <see cref="Random"/>.
    /// </summary>
    public static class Rand
    {
        /// <summary>
        /// The randomly seeded, single instance of <see cref="Random"/>.
        /// </summary>
        public static readonly Random Instance = new(Guid.NewGuid().GetHashCode());
    }
}
