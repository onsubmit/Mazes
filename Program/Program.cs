//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        public static void Main()
        {
            DeadEndCounts.GenerateReport();
        }

        private static void AldousBroderDemo()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20, 20);
                new AldousBroder().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"AldousBroder{i}.png", 20);
            }
        }

        private static void WilsonsDemo()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20, 20);
                new Wilsons().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"Wilson{i}.png", 20);
            }
        }

        private static void HuntAndKillDemo()
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