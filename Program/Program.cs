//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;
    using Library.Algorithms.Solving;

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
            AldousBroderDemo();
        }

        private static void AldousBroderDemo()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20, 20);
                AldousBroder.Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"AldousBroder{i}.png", 20);
            }

        }
    }
}