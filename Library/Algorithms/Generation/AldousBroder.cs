//-----------------------------------------------------------------------
// <copyright file="AldousBroder.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements the Aldous-Broder algorithm.
    /// </summary>
    public class AldousBroder : GenerationAlgorithm<CartesianGrid, Cell>
    {
        /// <summary>
        /// Executes the Aldous-Broder algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
        {
            int unvisitedCells = grid.Size - 1;

            Cell cell = grid.GetRandomCell();

            while (unvisitedCells > 0)
            {
                Cell neighbor = cell.Neighbors.GetRandomElement();

                if (!neighbor.Links.Any())
                {
                    cell.Link(neighbor);
                    unvisitedCells--;
                }

                cell = neighbor;
            }
        }
    }
}
