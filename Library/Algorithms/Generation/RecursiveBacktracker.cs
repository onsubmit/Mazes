﻿//-----------------------------------------------------------------------
// <copyright file="RecursiveBacktracker.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Cells;
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements the Recursive Backtracker algorithm.
    /// </summary>
    public class RecursiveBacktracker : GenerationAlgorithm<CartesianGrid, CartesianCell>
    {
        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
        {
            this.Execute(grid.GetRandomCell());
        }

        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        public void Execute(CartesianCell startingCell)
        {
            Stack<CartesianCell> stack = new();
            stack.Push(startingCell);

            while (stack.TryPeek(out CartesianCell? current))
            {
                IEnumerable<CartesianCell> unvisitedNeighbors = current.Neighbors.Where(n => !n.HasLink);

                if (!unvisitedNeighbors.Any())
                {
                    // We're boxed in.
                    // Pop the dead-end cell off the stack,
                    // which has the effect of making the previous cell our current cell again in the next iteration.
                    stack.Pop();
                    continue;
                }

                // Choose an unvisited neighbor.
                CartesianCell neighbor = unvisitedNeighbors.GetRandomElement();

                // Link it to the current cell.
                current.Link(neighbor);

                // Push it onto the stack, which makes it the current cell in the next iteration.
                stack.Push(neighbor);
            }
        }
    }
}
