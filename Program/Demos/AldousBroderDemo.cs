//-----------------------------------------------------------------------
// <copyright file="AldousBroderDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library.Algorithms.Generation;
    using Library.Grids;

    /// <summary>
    /// Class used to demonstrate the Aldous-Broder algorithm.
    /// </summary>
    public static class AldousBroderDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            for (int i = 0; i < 6; i++)
            {
                CartesianColoredGrid grid = new(20);
                new AldousBroder().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Values[grid.Rows / 2, grid.Columns / 2]);
                grid.SaveImage($"AldousBroder{i}.png", 20);
            }
        }
    }
}
