//-----------------------------------------------------------------------
// <copyright file="RecursiveBacktracker.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;

    /// <summary>
    /// Implements the Recursive Backtracker algorithm.
    /// </summary>
    public class RecursiveBacktracker : GenerationAlgorithm
    {
        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(Grid grid)
        {
            this.Execute(grid.GetRandomCell());
        }

        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        public void Execute(Cell startingCell)
        {
            Stack<Cell> stack = new();
            stack.Push(startingCell);

            while (stack.TryPeek(out Cell? current))
            {
                IEnumerable<Cell> unvisitedNeighbors = current.Neighbors.Where(n => !n.HasLink);

                if (!unvisitedNeighbors.Any())
                {
                    // We're boxed in.
                    // Pop the dead-end cell off the stack,
                    // which has the effect of making the previous cell our current cell again in the next iteration.
                    stack.Pop();
                    continue;
                }

                // Choose an unvisited neighbor.
                Cell neighbor = unvisitedNeighbors.GetRandomElement();

                // Link it to the current cell.
                current.Link(neighbor);

                // Push it onto the stack, which makes it the current cell in the next iteration.
                stack.Push(neighbor);
            }
        }
    }
}
