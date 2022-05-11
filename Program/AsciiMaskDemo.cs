//-----------------------------------------------------------------------
// <copyright file="AsciiMaskDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;

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
            Mask mask = Mask.FromFile("mask.txt");
            MaskedGrid grid = new(mask);

            new RecursiveBacktracker().Execute(grid);
            grid.SaveImage($"AsciiGrid.png", 20);
        }
    }
}
