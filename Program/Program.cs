//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;
    using Library.Algorithms.Solving;

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
            Console.WriteLine("Binary Tree");
            DistanceGrid binaryTreeGrid = new(5, 5);
            BinaryTree.Execute(binaryTreeGrid);
            LongestPath.Solve(binaryTreeGrid);
            binaryTreeGrid.SaveImage("BinaryTree.png", 40);

            Console.WriteLine("Sidewinder");
            DistanceGrid sideWinderGrid = new(5, 5);
            Sidewinder.Execute(sideWinderGrid);
            LongestPath.Solve(sideWinderGrid);
            sideWinderGrid.SaveImage("Sidewinder.png", 40);
        }
    }
}