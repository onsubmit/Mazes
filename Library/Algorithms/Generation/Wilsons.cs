//-----------------------------------------------------------------------
// <copyright file="Wilsons.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Cells;
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements Wilson's algorithm.
    /// </summary>
    public class Wilsons : GenerationAlgorithm<CartesianGrid, CartesianCell>
    {
        /// <summary>
        /// Executes Wilson's algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
        {
            List<CartesianCell> unvisited = new();
            grid.ForEachCell(cell => unvisited.Add(cell));

            CartesianCell first = unvisited.GetRandomElement();
            unvisited.Remove(first);

            while (unvisited.Any())
            {
                List<CartesianCell> path = new();
                CartesianCell cell = unvisited.GetRandomElement();
                path.Add(cell);

                while (unvisited.Contains(cell))
                {
                    cell = cell.Neighbors.GetRandomElement();
                    int position = path.IndexOf(cell);
                    if (position > 0)
                    {
                        path = path.GetRange(0, position + 1);
                    }
                    else
                    {
                        path.Add(cell);
                    }
                }

                for (int i = 0; i < path.Count - 1; i++)
                {
                    path[i].Link(path[i + 1]);
                    unvisited.Remove(path[i]);
                }
            }
        }
    }
}
