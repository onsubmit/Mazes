//-----------------------------------------------------------------------
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
    /// <typeparam name="TGrid">The type of grid.</typeparam>
    /// <typeparam name="TCell">The type of cell.</typeparam>
    public class RecursiveBacktracker<TGrid, TCell> : GenerationAlgorithm<TGrid, TCell>
        where TGrid : Grid<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(TGrid grid)
        {
            this.Execute(grid.GetRandomCell());
        }

        /// <summary>
        /// Executes the Recursive Backtracker algorithm.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        public void Execute(TCell startingCell)
        {
            Stack<TCell> stack = new();
            stack.Push(startingCell);

            while (stack.TryPeek(out TCell? current))
            {
                IEnumerable<TCell> unvisitedNeighbors = current.Neighbors.Cast<TCell>().Where(n => !n.HasLink);

                if (!unvisitedNeighbors.Any())
                {
                    // We're boxed in.
                    // Pop the dead-end cell off the stack,
                    // which has the effect of making the previous cell our current cell again in the next iteration.
                    stack.Pop();
                    continue;
                }

                // Choose an unvisited neighbor.
                TCell neighbor = unvisitedNeighbors.GetRandomElement();

                // Link it to the current cell.
                current.Link(neighbor);

                // Push it onto the stack, which makes it the current cell in the next iteration.
                stack.Push(neighbor);
            }
        }
    }
}
