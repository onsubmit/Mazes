//-----------------------------------------------------------------------
// <copyright file="PolarGridDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library.Algorithms.Generation;
    using Library.Cells;
    using Library.Grids;

    /// <summary>
    /// Class used to demonstrate a polar grid.
    /// </summary>
    public static class PolarGridDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            PolarColoredGrid grid = new(10);
            new RecursiveBacktracker<PolarGrid, PolarCell>().Execute(grid);

            grid.Distances = grid.GetDistancesFromCell(grid.Values[0][0]);
            grid.SaveImage("PolarGrid.png", 40);
        }
    }
}
