//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        public static void Main()
        {
            Grid grid = new(10, 10);
            grid.ForEachRow(row => Console.WriteLine(string.Join<Cell>(",", row)));

            Console.WriteLine("Done");
        }
    }
}