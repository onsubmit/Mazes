//-----------------------------------------------------------------------
// <copyright file="IGenerationAlgorithm.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    /// <summary>
    /// Interface for the maze generation algorithms.
    /// </summary>
    public interface IGenerationAlgorithm
    {
        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        void Execute(Grid grid);
    }
}
