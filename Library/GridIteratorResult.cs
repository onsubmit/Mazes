//-----------------------------------------------------------------------
// <copyright file="GridIteratorResult.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Result returned by a grid iterator function.
    /// </summary>
    public enum GridIteratorResult
    {
        /// <summary>
        /// Continue executing.
        /// </summary>
        Continue,

        /// <summary>
        /// Stop executing.
        /// </summary>
        Stop,
    }
}
