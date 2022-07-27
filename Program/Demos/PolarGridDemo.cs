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
            PolarColoredGrid grid = new(20);
            new RecursiveBacktracker<PolarGrid, PolarCell>().Execute(grid);

            int row = grid.Rows / 2;
            int column = grid.Values[row].Count / 2;
            grid.Distances = grid.GetDistancesFromCell(grid.Values[row][column]);
            grid.SaveImage("PolarGrid.png", 100);
        }
    }
}
