//-----------------------------------------------------------------------
// <copyright file="HuntAndKill.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements the Hunt-and-Kill algorithm.
    /// </summary>
    public class HuntAndKill : GenerationAlgorithm<CartesianGrid, Cell>
    {
        /// <summary>
        /// Executes the Hunt-and-Kill algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
        {
            // Begin and some random cell.
            Cell? current = grid.GetRandomCell();

            // Perform random walk.
            while (current != null)
            {
                IEnumerable<Cell> unvisitedNeighbors = current.Neighbors.Where(n => !n.HasLink);
                if (unvisitedNeighbors.Any())
                {
                    // Avoid cells we've already visited.
                    Cell unvisitedNeighbor = unvisitedNeighbors.GetRandomElement();
                    current.Link(unvisitedNeighbor);
                    current = unvisitedNeighbor;
                }
                else
                {
                    // All neighbors are visited, we've painted ourselves into a corner.
                    current = null;

                    // Find the first unvisited cell that is bordered by at least one visited cell.
                    grid.ForEachCell(cell =>
                    {
                        if (cell.HasLink)
                        {
                            return IteratorResult.Continue;
                        }

                        IEnumerable<Cell> visitedNeighbors = cell.Neighbors.Where(n => n.HasLink);
                        if (visitedNeighbors.Any())
                        {
                            current = cell;

                            Cell visitedNeighbor = visitedNeighbors.GetRandomElement();
                            current.Link(visitedNeighbor);

                            return IteratorResult.Stop;
                        }

                        return IteratorResult.Continue;
                    });
                }
            }
        }
    }
}
