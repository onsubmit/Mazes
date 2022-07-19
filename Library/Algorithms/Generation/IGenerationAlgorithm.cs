//-----------------------------------------------------------------------
// <copyright file="IGenerationAlgorithm.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Grids;

    /// <summary>
    /// Interface for the maze generation algorithms.
    /// </summary>
    /// <typeparam name="TGrid">The type of grid.</typeparam>
    /// <typeparam name="TCell">The type of cell.</typeparam>
    public interface IGenerationAlgorithm<TGrid, TCell>
        where TGrid : Grid<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        void Execute(TGrid grid);
    }
}
