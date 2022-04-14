//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;

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
            DistanceGrid binaryTreeGrid = new(10, 10);
            BinaryTree.Execute(binaryTreeGrid);

            binaryTreeGrid.SetDistancesFromCell(0, 0);
            Console.WriteLine(binaryTreeGrid);
            binaryTreeGrid.SaveImage("BinaryTree.png", 40);

            DistanceGrid sideWinderGrid = new(10, 10);
            Sidewinder.Execute(sideWinderGrid);

            sideWinderGrid.SetDistancesFromCell(0, 0);
            Console.WriteLine(sideWinderGrid);
            sideWinderGrid.SaveImage("Sidewinder.png", 40);
        }
    }
}