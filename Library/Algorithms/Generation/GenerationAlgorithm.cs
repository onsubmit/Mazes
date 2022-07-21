//-----------------------------------------------------------------------
// <copyright file="GenerationAlgorithm.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Cells;
    using Library.Grids;

    /// <summary>
    /// Base abstract implementation of <see cref="IGenerationAlgorithm{TGrid, TCell}"/>.
    /// </summary>
    /// <typeparam name="TGrid">The type of grid.</typeparam>
    /// <typeparam name="TCell">The type of cell.</typeparam>
    public abstract class GenerationAlgorithm<TGrid, TCell> : IGenerationAlgorithm<TGrid, TCell>
        where TGrid : Grid<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Gets the algorithm's name.
        /// </summary>
        public string Name => this.GetType().Name;

        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public abstract void Execute(TGrid grid);
    }
}
