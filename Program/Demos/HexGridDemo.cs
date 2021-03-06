//-----------------------------------------------------------------------
// <copyright file="HexGridDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library.Algorithms.Generation;
    using Library.Cells;
    using Library.Grids;

    /// <summary>
    /// Class used to demonstrate a hex grid.
    /// </summary>
    public static class HexGridDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            HexColoredGrid grid = new(30);
            new RecursiveBacktracker<HexGrid, HexCell>().Execute(grid);

            grid.Distances = grid.GetDistancesFromCell(grid.GetRandomCell());
            grid.SaveImage("HexGrid.png", 20);
        }
    }
}
