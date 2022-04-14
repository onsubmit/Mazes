﻿//-----------------------------------------------------------------------
// <copyright file="Sidewinder.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    using Library.Extensions;

    /// <summary>
    /// Implements the Sidewinder algorithm.
    /// </summary>
    public static class Sidewinder
    {
        /// <summary>
        /// Executes the Sidewinder algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public static void Execute(Grid grid)
        {
            grid.ForEachRow(row =>
            {
                List<Cell> run = new();
                foreach (Cell cell in row)
                {
                    run.Add(cell);

                    if (cell.East == null || (cell.North != null && Rand.Instance.Next(2) == 0))
                    {
                        // Close out the current run if we're at the eastern boundary
                        // or randomly as long as we are not in the northernmost row.
                        if (run.TryGetRandomElement(out Cell member) && member.North != null)
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
