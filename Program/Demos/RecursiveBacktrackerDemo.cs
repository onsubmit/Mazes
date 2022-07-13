//-----------------------------------------------------------------------
// <copyright file="RecursiveBacktrackerDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library;
    using Library.Algorithms.Generation;

    /// <summary>
    /// Class used to demonstrate the Recursive Backtracker algorithm.
    /// </summary>
    public static class RecursiveBacktrackerDemo
    {
        /// <summary>
        /// Generates the report.
        /// </summary>
        public static void Execute()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20);
                new RecursiveBacktracker().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"RecursiveBacktracker{i}.png", 20);
            }
        }
    }
}
