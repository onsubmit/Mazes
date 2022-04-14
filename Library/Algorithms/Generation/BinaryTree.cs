﻿//-----------------------------------------------------------------------
// <copyright file="BinaryTree.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;

    /// <summary>
    /// Implements the Binary Tree algorithm.
    /// </summary>
    public static class BinaryTree
    {
        /// <summary>
        /// Executes the Binary Tree algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public static void Execute(Grid grid)
        {
            grid.ForEachCell(cell =>
            {
                List<Cell> neighbors = new();
                neighbors.AddIfNotNull(cell.North);
                neighbors.AddIfNotNull(cell.East);

                if (neighbors.TryGetRandomElement(out Cell neighbor))
                {
                    cell.Link(neighbor);
                }
            });
        }
    }
}
