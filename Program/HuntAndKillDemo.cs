//-----------------------------------------------------------------------
// <copyright file="HuntAndKillDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;

    /// <summary>
    /// Class used to demonstrate the Hunt and Kill algorithm.
    /// </summary>
    public static class HuntAndKillDemo
    {
        /// <summary>
        /// Generates the report.
        /// </summary>
        public static void Execute()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20, 20);
                new HuntAndKill().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"HuntAndKill{i}.png", 20);
            }
        }
    }
}
