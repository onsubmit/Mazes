//-----------------------------------------------------------------------
// <copyright file="Dijkstra.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Solving
{
    /// <summary>
    /// Implements Dijkstra's maze solving algorithm.
    /// </summary>
    public static class Dijkstra
    {
        /// <summary>
        /// Solves the maze using Dijkstra's algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public static void Solve(DistanceGrid grid)
        {
            grid.Distances = grid.GetDistancesFromCell(0, 0);
            Console.WriteLine(grid);

            grid.Distances = grid.Distances.GetShortestPathTo(grid.Cells[grid.Rows - 1, 0]);
            Console.WriteLine(grid);
        }
    }
}
