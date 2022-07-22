//-----------------------------------------------------------------------
// <copyright file="ImageMaskDemo.cs" company="Andy Young">
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
    public static class ImageMaskDemo
    {
        /// <summary>
        /// Executes the demo.
        /// </summary>
        public static void Execute()
        {
            ImageMask mask = ImageMask.FromFile("mandelbrot.png");
            MaskedGrid grid = new(mask);

            new RecursiveBacktracker<CartesianGrid, CartesianCell>().Execute(grid);

            Cell startingCell = grid.GetRandomCell();
            grid.Distances = startingCell.GetDistances();

            grid.SaveImage($"ImageMask.png", 5);
        }
    }
}
