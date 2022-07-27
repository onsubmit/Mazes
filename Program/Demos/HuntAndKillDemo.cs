//-----------------------------------------------------------------------
// <copyright file="HuntAndKillDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library.Algorithms.Generation;
    using Library.Grids;

    /// <summary>
    /// Class used to demonstrate the Hunt and Kill algorithm.
    /// </summary>
    public static class HuntAndKillDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20);
                new HuntAndKill().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Values[grid.Rows / 2, grid.Columns / 2]);
                grid.SaveImage($"HuntAndKill{i}.png", 20);
            }
        }
    }
}
