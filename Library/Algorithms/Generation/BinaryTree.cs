//-----------------------------------------------------------------------
// <copyright file="BinaryTree.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Cells;
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements the Binary Tree algorithm.
    /// </summary>
    public class BinaryTree : GenerationAlgorithm<CartesianGrid, CartesianCell>
    {
        /// <summary>
        /// Executes the Binary Tree algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
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
