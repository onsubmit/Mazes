//-----------------------------------------------------------------------
// <copyright file="IteratorResult.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Result returned by a two dimensional array iterator function.
    /// </summary>
    public enum IteratorResult
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
