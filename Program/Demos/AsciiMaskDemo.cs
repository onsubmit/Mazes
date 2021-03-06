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
            CartesianMaskedGrid grid = new(mask);

            new RecursiveBacktracker<CartesianMaskedGrid, CartesianCell>().Execute(grid);

            CartesianCell startingCell = grid.GetRandomCell();
            grid.Distances = startingCell.GetDistances<CartesianCell>();

            grid.SaveImage($"AsciiGrid.png", 40);
        }
    }
}
