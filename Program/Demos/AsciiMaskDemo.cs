//-----------------------------------------------------------------------
// <copyright file="AsciiMaskDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library.Algorithms.Generation;
    using Library.Cells;
    using Library.Grids;
    using Library.Masks;

    /// <summary>
    /// Demonstrates the <see cref="Mask"/> class using an ASCII file.
    /// </summary>
    public static class AsciiMaskDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            AsciiMask mask = AsciiMask.FromFile("mask.txt");
            MaskedGrid grid = new(mask);

            new RecursiveBacktracker<CartesianGrid, CartesianCell>().Execute(grid);

            Cell startingCell = grid.GetRandomCell();
            grid.Distances = startingCell.GetDistances();

            grid.SaveImage($"AsciiGrid.png", 40);
        }
    }
}
