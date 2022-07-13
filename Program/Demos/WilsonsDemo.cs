//-----------------------------------------------------------------------
// <copyright file="WilsonsDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library;
    using Library.Algorithms.Generation;

    /// <summary>
    /// Class used to demonstrate the Wilsons algorithm.
    /// </summary>
    public static class WilsonsDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            for (int i = 0; i < 6; i++)
            {
                ColoredGrid grid = new(20);
                new Wilsons().Execute(grid);
                grid.Distances = grid.GetDistancesFromCell(grid.Rows / 2, grid.Columns / 2);
                grid.SaveImage($"Wilson{i}.png", 20);
            }
        }
    }
}
