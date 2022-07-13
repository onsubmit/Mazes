//-----------------------------------------------------------------------
// <copyright file="AsciiMaskDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
    using Library;
    using Library.Algorithms.Generation;
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

            new RecursiveBacktracker().Execute(grid);

            Cell startingCell = grid.GetRandomElement();
            grid.Distances = startingCell.GetDistances();

            grid.SaveImage($"AsciiGrid.png", 40);
        }
    }
}
