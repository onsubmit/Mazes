//-----------------------------------------------------------------------
// <copyright file="Sidewinder.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Cells;
    using Library.Extensions;
    using Library.Grids;

    /// <summary>
    /// Implements the Sidewinder algorithm.
    /// </summary>
    public class Sidewinder : GenerationAlgorithm<CartesianGrid, CartesianCell>
    {
        /// <summary>
        /// Executes the Sidewinder algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public override void Execute(CartesianGrid grid)
        {
            grid.ForEachRow(row =>
            {
                List<CartesianCell> run = new();
                foreach (CartesianCell cell in row)
                {
                    run.Add(cell);

                    if (cell.East == null || (cell.North != null && Rand.Instance.Next(2) == 0))
                    {
                        // Close out the current run if we're at the eastern boundary
                        // or randomly as long as we are not in the northernmost row.
                        CartesianCell member = run.GetRandomElement();
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }

                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            });
        }
    }
}
