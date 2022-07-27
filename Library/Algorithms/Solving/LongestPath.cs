//-----------------------------------------------------------------------
// <copyright file="LongestPath.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Library.Algorithms.Solving
{
    using Library.Cells;
    using Library.Grids;

    /// <summary>
    /// Implements the longest path maze solving algorithm.
    /// </summary>
    public static class LongestPath
    {
        /// <summary>
        /// Solves the maze using the longest path maze solving algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public static void Solve(CartesianDistanceGrid grid)
        {
            grid.Distances = grid.GetDistancesFromCell(grid.Values[0, 0]);

            Cell furthest = grid.Distances.GetCellFurthestFromRoot(out int _);
            grid.Distances = furthest.GetDistances<CartesianCell>();
            Console.WriteLine(grid);

            CartesianCell goal = grid.Distances.GetCellFurthestFromRoot(out int _);
            grid.Distances = grid.Distances.GetShortestPathTo(goal);

            Console.WriteLine(grid);
        }
    }
}
