//-----------------------------------------------------------------------
// <copyright file="PolarGridDemo.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program.Demos
{
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
            PolarGrid grid = new(16);
            grid.SaveImage("polar.png", 50);
        }
    }
}
