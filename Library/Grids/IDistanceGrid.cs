//-----------------------------------------------------------------------
// <copyright file="IDistanceGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;

    /// <summary>
    /// Interface for grids that can calculate the shortest distance between its cells.
    /// </summary>
    /// <typeparam name="TCell">The type of cells the grid contains.</typeparam>
    public interface IDistanceGrid<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Gets or sets the distances.
        /// </summary>
        Distances<TCell>? Distances { get; set; }

        /// <summary>
        /// Gets the distances from the given cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The distances from the given cell.</returns>
        Distances<TCell> GetDistancesFromCell(TCell cell);
    }
}
