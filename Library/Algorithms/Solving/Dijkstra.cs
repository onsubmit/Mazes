//-----------------------------------------------------------------------
// <copyright file="Dijkstra.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Solving
{
    using Library.Grids;

    /// <summary>
    /// Implements Dijkstra's maze solving algorithm.
    /// </summary>
    public static class Dijkstra
    {
        /// <summary>
        /// Solves the maze using Dijkstra's algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="end">The ending cell.</param>
        public static void Solve(DistanceGrid grid, Cell start, Cell end)
        {
            grid.Distances = start.GetDistances();
            Console.WriteLine(grid);

            grid.Distances = grid.Distances.GetShortestPathTo(end);
            Console.WriteLine(grid);
        }
    }
}
