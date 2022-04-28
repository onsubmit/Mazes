//-----------------------------------------------------------------------
// <copyright file="DeadEndCounts.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Program
{
    using Library;
    using Library.Algorithms.Generation;

    /// <summary>
    /// Class used to generate a report of the average number of dead ends for each algorithm.
    /// </summary>
    public static class DeadEndCounts
    {
        private const int Tries = 100;
        private const int Size = 20;
        private const int TotalCells = Size * Size;

        private static readonly GenerationAlgorithm[] Algorithms =
            new GenerationAlgorithm[]
            {
                new BinaryTree(),
                new Sidewinder(),
                new AldousBroder(),
                new Wilsons(),
                new HuntAndKill(),
            };

        /// <summary>
        /// Generates the report.
        /// </summary>
        public static void GenerateReport()
        {
            Dictionary<GenerationAlgorithm, int> averages = new();
            foreach (GenerationAlgorithm algorithm in Algorithms)
            {
                Console.WriteLine($"Running {algorithm.Name}...");

                List<int> deadEndCounts = new();
                for (int i = 0; i < Tries; i++)
                {
                    Grid grid = new(Size);
                    algorithm.Execute(grid);
                    deadEndCounts.Add(grid.GetDeadEnds().Count);
                }

                int totalDeadEnds = deadEndCounts.Sum();
                averages[algorithm] = totalDeadEnds / deadEndCounts.Count;
            }

            Console.WriteLine();
            Console.WriteLine($"Average dead-ends per {Size}x{Size} maze ({TotalCells} cells):");
            Console.WriteLine();

            Dictionary<GenerationAlgorithm, int> sortedAverages = averages
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach ((GenerationAlgorithm algorithm, int value) in sortedAverages)
            {
                float percentage = value * 100.0f / TotalCells;
                Console.WriteLine("{0,14} : {1,3}/{2} ({3:0}%)", algorithm.Name, value, TotalCells, percentage);
            }
        }
    }
}
