//-----------------------------------------------------------------------
// <copyright file="Wilsons.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;

    /// <summary>
    /// Implements Wilson's algorithm.
    /// </summary>
    public class Wilsons : GenerationAlgorithm
    {
        /// <summary>
        /// Executes Wilson's algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(Grid grid)
        {
            List<Cell> unvisited = new();
            grid.ForEachCell(cell => unvisited.Add(cell));

            Cell first = unvisited.GetRandomElement();
            unvisited.Remove(first);

            while (unvisited.Any())
            {
                List<Cell> path = new();
                Cell cell = unvisited.GetRandomElement();
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
