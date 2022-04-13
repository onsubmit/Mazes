//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms;

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
            Grid binaryTreeGrid = new(20, 20);
            BinaryTree.Execute(binaryTreeGrid);
            binaryTreeGrid.SaveImage("BinaryTree.png", 20);

            Grid sideWinderGrid = new(20, 20);
            Sidewinder.Execute(sideWinderGrid);
            sideWinderGrid.SaveImage("Sidewinder.png", 20);
        }
    }
}