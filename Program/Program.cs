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
            ColoredGrid binaryTreeGrid = new(25, 25);
            BinaryTree.Execute(binaryTreeGrid);
            Cell start = binaryTreeGrid.Cells[binaryTreeGrid.Rows - 1, 4];
            Cell end = binaryTreeGrid.Cells[8, binaryTreeGrid.Columns - 1];
            binaryTreeGrid.Distances = start.GetDistances();
            binaryTreeGrid.SaveImage("ColoredBinaryTree.png", 20);
            Dijkstra.Solve(binaryTreeGrid, start, end);
            binaryTreeGrid.SaveImage("ColoredBinaryTreeDijkstra.png", 20);

            Console.WriteLine("Sidewinder");
            ColoredGrid sideWinderGrid = new(25, 25);
            Sidewinder.Execute(sideWinderGrid);
            start = sideWinderGrid.Cells[sideWinderGrid.Rows - 1, 4];
            end = sideWinderGrid.Cells[8, sideWinderGrid.Columns - 1];
            sideWinderGrid.Distances = start.GetDistances();
            sideWinderGrid.SaveImage("ColoredSidewinder.png", 20);
            Dijkstra.Solve(sideWinderGrid, start, end);
            sideWinderGrid.SaveImage("ColoredSidewinderDijkstra.png", 20);
        }
    }
}